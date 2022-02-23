using Microsoft.ML;
using Microsoft.ML.Trainers.FastTree;
using ML.AiModels.Regression;
using ML.Common.MlRepository;
using ML.Core;

namespace ML.Services.Regression
{
    /// <summary>
    /// Service layer for managing FastTree regression models
    /// </summary>
    public class FastTreeRegressionService : RegressionServiceBase, IFastTreeRegressionService
    {
        protected MLContext _mLContext;
        IFastTreeModelBuilderFactory _builderFactory;


        public FastTreeRegressionService(
                IFastTreeModelBuilderFactory modelBuilderFactory,
                IRegressionPredictionEngine predictionEngine,
                IMlRepository mLDataLoader,
                MLContext mLContext) : base(predictionEngine, mLDataLoader)
        {
            _builderFactory = modelBuilderFactory;
            _mLContext = mLContext;
            ModelType = MlAlgorithm.FastTree;
            ModelCategory = MlCategory.Regression;
        }

        /// <summary>
        /// Creates FastTree regression model
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Created model performance metrics</returns>
        public RegressionServiceCreateModelOutput Create(CreateFastTreeRegressionModelOptions options)
        {
            IModelBuilder<FastTreeRegressionModelBuilderOptions> modelBuilder  = _builderFactory.CreateFastTreeModelBuilder(_mLContext);

            var builderOptions = new FastTreeRegressionModelBuilderOptions()
            {
                ModelName = options.ModelName,
                LabelColumnName = options.LabelColumnName,
                FeatureColumnNames = options.FeatureColumnNames,
                TrainingDataName = options.TrainingDataName,
                CrossValidationFoldsCount = options.CrossValidationFoldsCount,
                PermutationCount = options.PermutationCount,
                HasFeatureContributionMetrics = options.HasFeatureContributionMetrics,

                TrainerOptions = new FastTreeRegressionTrainer.Options
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
