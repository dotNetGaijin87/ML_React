using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static Microsoft.ML.TrainCatalogBase;

namespace ML.AiModels.Regression
{
    /// <summary>
    /// Generic regression model builder
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="TTrainer"></typeparam>
    /// <typeparam name="TParams"></typeparam>
    public class GenericRegressionModelBuilder<TOptions, TTrainer,TParams> : IModelBuilder<TOptions>
        where TOptions: RegressionModelBuilderOptionsBase
        where TTrainer: IEstimator<RegressionPredictionTransformer<TParams>> 
        where TParams : class
    {
        private readonly MLContext _mlContext;
        Func<MLContext, TOptions, TTrainer> _trainerCreator;

        public GenericRegressionModelBuilder(MLContext mlContext,Func<MLContext,TOptions, TTrainer> trainerCreator)
        {
            _mlContext = mlContext;
            _trainerCreator = trainerCreator;
        }

        /// <summary>
        /// Builds the ml model
        /// </summary>
        /// <param name="trainingData"></param>
        /// <param name="options"></param>
        /// <returns>Ml model with performance metrics</returns>
        public (ITransformer, RegressionModelBuilderOutput) Build(IDataView trainingData, TOptions options)
        {
            NormalizingEstimator normalizingEstimator = null;

            // Common pipline consisting of only normalized float values
            for (int i = 0; i < options.FeatureColumnNames.Count(); i++)
            {
                if(i == 0)
                {
                    normalizingEstimator = _mlContext.Transforms.NormalizeMeanVariance(options.FeatureColumnNames[i]);
                }
                else
                {
                    normalizingEstimator.Append(_mlContext.Transforms.NormalizeMeanVariance(options.FeatureColumnNames[i]));
                }
            }

            EstimatorChain<ColumnConcatenatingTransformer> preTrainingPipline = normalizingEstimator.Append(_mlContext.Transforms.Concatenate(options.FeaturesName, options.FeatureColumnNames));
            ColumnConcatenatingTransformer transformer = preTrainingPipline.LastEstimator.Fit(trainingData);
            IDataView transformedData = transformer.Transform(trainingData);
            TTrainer trainer = _trainerCreator(_mlContext, options);

            // Ml model with feature contribution calcualtion added to the pipline
            ITransformer mlModel = CreateModelWithFeatureContributionMetrics(transformedData, transformer, trainer);

            var performanceMetrics = new RegressionModelBuilderOutput
            {
                FeatureImportanceList = CalculatePermutationFeatureImportance(transformedData, options, trainer),
                ValidationResults = CalculateCrossValidation(trainingData, options, preTrainingPipline, trainer),
                ContributingFeatureIndexes = MlNetRegressionHelpers.TryGetFeatureColumnNames(mlModel.GetOutputSchema(trainingData.Schema), options.FeaturesName)
            };


            return (mlModel, performanceMetrics);
        }

        /// <summary>
        /// Calculates feature contribution metrics for each selected feature
        /// </summary>
        /// <param name="trainingData"></param>
        /// <param name="transformer"></param>
        /// <param name="trainer"></param>
        /// <returns></returns>
        private ITransformer CreateModelWithFeatureContributionMetrics(
            IDataView trainingData,
            ColumnConcatenatingTransformer transformer, 
            IEstimator<ITransformer> trainer)
        {

            ITransformer linearModel = trainer.Fit(trainingData);
            FeatureContributionCalculatingTransformer linearFeatureContributionCalculator = _mlContext.Transforms
                                        .CalculateFeatureContribution((ISingleFeaturePredictionTransformer<ICalculateFeatureContribution>)linearModel, normalize: true)
                                        .Fit(trainingData);

            ITransformer mlModel = transformer.Append((ISingleFeaturePredictionTransformer<ICalculateFeatureContribution>)linearModel)
                                            .Append(linearFeatureContributionCalculator);

            return mlModel;
        }

        /// <summary>
        /// Calculates feature permutation importance for each selected feature
        /// </summary>
        /// <param name="trainingData"></param>
        /// <param name="settings"></param>
        /// <param name="trainer"></param>
        /// <returns></returns>
        private ImmutableArray<RegressionMetricsStatistics> CalculatePermutationFeatureImportance(
            IDataView trainingData,
            TOptions settings,
            IEstimator<ITransformer> trainer)
        {
 
            ITransformer mlModel = trainer.Fit(trainingData);
            var typedModel = (RegressionPredictionTransformer<TParams>)mlModel;
            return _mlContext.Regression.PermutationFeatureImportance(
                                                                    typedModel,
                                                                    trainingData,
                                                                    labelColumnName: settings.LabelColumnName,
                                                                    permutationCount: settings.PermutationCount);
        }

        /// <summary>
        /// Evaluates model performance with cross validation
        /// </summary>
        /// <param name="trainingData"></param>
        /// <param name="options"></param>
        /// <param name="preTrainingPipline"></param>
        /// <param name="trainer"></param>
        /// <returns></returns>
        private IReadOnlyList<CrossValidationResult<RegressionMetrics>> CalculateCrossValidation(
            IDataView trainingData,
            TOptions options, 
            EstimatorChain<ColumnConcatenatingTransformer> preTrainingPipline,
            IEstimator<ITransformer> trainer)
        {
            var trainingPipeline = preTrainingPipline.Append(trainer);

            return _mlContext.Regression.CrossValidate(
                                            trainingData,
                                            trainingPipeline,
                                            numberOfFolds: options.CrossValidationFoldsCount,
                                            labelColumnName: options.LabelColumnName);
        }

    }
}
