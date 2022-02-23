using Microsoft.ML;
using Microsoft.ML.Trainers;
using Microsoft.ML.Trainers.FastTree;

namespace ML.AiModels.Regression
{
    public class RegressionModelFactory : 
        IFastForestModelBuilderFactory,
        ISdcaModelBuilderFactory, 
        IFastTreeModelBuilderFactory,
        IOnlineGradientDescentModelBuilderFactory, 
        IGamModelBuilderFactory, 
        IFastTreeTweedieModelBuilderFactory, 
        ILbfgsPoissonRegressionModelBuilderFactory
    {
        public IModelBuilder<FastForestRegressionModelBuilderOptions> CreateFastForestModelBuilder(MLContext mlContext)
        {
            return new GenericRegressionModelBuilder<FastForestRegressionModelBuilderOptions, FastForestRegressionTrainer, FastForestRegressionModelParameters>(
                        mlContext,
                        (mlContext, options) => mlContext.Regression.Trainers
                                                    .FastForest(options.TrainerOptions));
        }

        public IModelBuilder<SdcaRegressionModelBuilderOptions> CreateSdcaModelBuilder(MLContext mlContext)
        {
            return new GenericRegressionModelBuilder<SdcaRegressionModelBuilderOptions, SdcaRegressionTrainer, LinearRegressionModelParameters>(
                        mlContext,
                        (mlContext, options) => mlContext.Regression.Trainers
                                                    .Sdca(options.TrainerOptions));
        }

        public IModelBuilder<FastTreeRegressionModelBuilderOptions> CreateFastTreeModelBuilder(MLContext mlContext)
        {
            return new GenericRegressionModelBuilder<FastTreeRegressionModelBuilderOptions, FastTreeRegressionTrainer, FastTreeRegressionModelParameters>(
                        mlContext,
                        (mlContext, options) => mlContext.Regression.Trainers
                                                    .FastTree(options.TrainerOptions));
        }

        public IModelBuilder<SdcaRegressionModelBuilderOptions> CreateOnlineGradientDescentModelBuilder(MLContext mlContext)
        {
            return new GenericRegressionModelBuilder<SdcaRegressionModelBuilderOptions, OnlineGradientDescentTrainer, LinearRegressionModelParameters>(
                        mlContext,
                        (mlContext, options) => mlContext.Regression.Trainers
                                                    .OnlineGradientDescent(
                                                        featureColumnName: options.FeaturesName,
                                                        labelColumnName: options.LabelColumnName ));
        }

        public IModelBuilder<SdcaRegressionModelBuilderOptions> CreateGamModelBuilder(MLContext mlContext)
        {
            return new GenericRegressionModelBuilder<SdcaRegressionModelBuilderOptions, GamRegressionTrainer, GamRegressionModelParameters>(
                        mlContext,
                        (mlContext, options) => mlContext.Regression.Trainers
                                                    .Gam(
                                                        featureColumnName: options.FeaturesName,
                                                        labelColumnName: options.LabelColumnName));
        }

        public IModelBuilder<SdcaRegressionModelBuilderOptions> CreateFastTreeTweedieModelBuilder(MLContext mlContext)
        {
            return new GenericRegressionModelBuilder<SdcaRegressionModelBuilderOptions, FastTreeTweedieTrainer, FastTreeTweedieModelParameters>(
                        mlContext,
                        (mlContext, options) => mlContext.Regression.Trainers
                                                    .FastTreeTweedie(
                                                        featureColumnName: options.FeaturesName,
                                                        labelColumnName: options.LabelColumnName));
        }

        public IModelBuilder<SdcaRegressionModelBuilderOptions> CreateLbfgsPoissonModelBuilder(MLContext mlContext)
        {
            return new GenericRegressionModelBuilder<SdcaRegressionModelBuilderOptions, LbfgsPoissonRegressionTrainer, PoissonRegressionModelParameters>(
                        mlContext,
                        (mlContext, options) => mlContext.Regression.Trainers
                                                    .LbfgsPoissonRegression(
                                                        featureColumnName: options.FeaturesName,
                                                        labelColumnName: options.LabelColumnName));
        }
    }
}
