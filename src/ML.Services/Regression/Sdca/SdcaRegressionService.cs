using Microsoft.ML;
using Microsoft.ML.Trainers;
using ML.AiModels.Regression;
using ML.Common.MlRepository;
using ML.Core;

namespace ML.Services.Regression
{
    /// <summary>
    /// Service layer for managing Sdca regression models
    /// </summary>
    public class SdcaRegressionService : RegressionServiceBase, ISdcaRegressionService
    {
        protected MLContext _mLContext;
        ISdcaModelBuilderFactory _builderFactory;

        public SdcaRegressionService(
                ISdcaModelBuilderFactory modelBuilderFactory,
                IRegressionPredictionEngine predictionEngine,
                IMlRepository mLDataLoader,
                MLContext mLContext) : base(predictionEngine, mLDataLoader)
        {
            _builderFactory = modelBuilderFactory;
            _mLContext = mLContext;
            ModelType = MlAlgorithm.Sdca;
            ModelCategory = MlCategory.Regression;
        }

        /// <summary>
        /// Creates Sdca regression model
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Created model performance metrics</returns>
        public RegressionServiceCreateModelOutput Create(CreateSdcaRegressionModelOptions options)
        {
            IModelBuilder<SdcaRegressionModelBuilderOptions> modelBuilder = _builderFactory.CreateSdcaModelBuilder(_mLContext);

            var builderOptions = new SdcaRegressionModelBuilderOptions()
            {
                ModelName = options.ModelName,
                LabelColumnName = options.LabelColumnName,
                FeatureColumnNames = options.FeatureColumnNames,
                TrainingDataName = options.TrainingDataName,
                CrossValidationFoldsCount = options.CrossValidationFoldsCount,
                PermutationCount = options.PermutationCount,
                HasFeatureContributionMetrics = options.HasFeatureContributionMetrics,

                TrainerOptions = new SdcaRegressionTrainer.Options
                {
                    LabelColumnName = options.LabelColumnName,
                    BiasLearningRate = options.BiasLearningRate,
                    ConvergenceTolerance = options.ConvergenceTolerance,
                    MaximumNumberOfIterations = options.MaximumNumberOfIterations,
                }
            };
 

            return base.CreateModel(modelBuilder, builderOptions);
        }
    }
}
