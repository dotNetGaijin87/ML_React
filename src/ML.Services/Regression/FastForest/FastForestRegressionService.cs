using Microsoft.ML;
using Microsoft.ML.Trainers.FastTree;
using ML.AiModels.Regression;
using ML.Common.MlRepository;
using ML.Core;

namespace ML.Services.Regression
{
    /// <summary>
    /// Service layer for managing FastForest regression models
    /// </summary>
    public class FastForestRegressionService : RegressionServiceBase, IFastForestRegressionService 
    {
        protected MLContext _mLContext;
        IFastForestModelBuilderFactory _builderFactory;

        public FastForestRegressionService(
                IFastForestModelBuilderFactory modelBuilderFactory,
                IRegressionPredictionEngine predictionEngine,
                IMlRepository mLDataLoader,
                MLContext mLContext) : base(predictionEngine, mLDataLoader)
        {
            _builderFactory = modelBuilderFactory;
            _mLContext = mLContext;
            ModelType = MlAlgorithm.FastForest;
            ModelCategory = MlCategory.Regression;
        }

        /// <summary>
        /// Creates FastForest regression model
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Created model performance metrics</returns>
        public RegressionServiceCreateModelOutput Create(CreateFastForestRegressionDto options)
        {
            IModelBuilder<FastForestRegressionModelBuilderOptions> modelBuilder = _builderFactory.CreateFastForestModelBuilder(_mLContext);

            var builderOptions = new FastForestRegressionModelBuilderOptions()
            {
                ModelName = options.ModelName,
                LabelColumnName = options.LabelColumnName,
                FeatureColumnNames = options.FeatureColumnNames,
                TrainingDataName = options.TrainingDataName,
                CrossValidationFoldsCount = options.CrossValidationFoldsCount,
                PermutationCount = options.PermutationCount,
                HasFeatureContributionMetrics = options.HasFeatureContributionMetrics,

                TrainerOptions = new FastForestRegressionTrainer.Options
                {
                    LabelColumnName = options.LabelColumnName,
                    NumberOfTrees = options.TreesCount,
                    NumberOfLeaves = options.LeavesCount,
                    MinimumExampleCountPerLeaf = options.MinimumExampleCountPerLeaf
                }
            };


            return base.CreateModel(modelBuilder, builderOptions);
        }
    }
}
