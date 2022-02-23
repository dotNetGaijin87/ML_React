using Microsoft.ML;
using Microsoft.ML.Data;
using ML.AiModels.Common;
using ML.AiModels.Regression;
using ML.Common.DataPathRegister;
using ML.Common.MlRepository;
using ML.Services.Regression;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.ML.TrainCatalogBase;

namespace ML.Tests.ML.Services.Regression
{
    [TestFixture]
    public class FastTreeServiceTests
    {
        IFastTreeModelBuilderFactory _fakeModelBuilderFactory;
        IRegressionPredictionEngine _fakePredictionEngine;
        IMlRepository _fakeMlDataLoader;
        IDataPathRegister _fakeDataPathRegister;
        IModelBuilder<FastTreeRegressionModelBuilderOptions> _fakeModelBuilder;


        private void ReinitializeFakes()
        {
            _fakeModelBuilderFactory = Substitute.For<IFastTreeModelBuilderFactory>();
            _fakePredictionEngine = Substitute.For<IRegressionPredictionEngine>();
            _fakeMlDataLoader = Substitute.For<IMlRepository>();
            _fakeDataPathRegister = Substitute.For<IDataPathRegister>();
            _fakeModelBuilder = Substitute.For<IModelBuilder<FastTreeRegressionModelBuilderOptions>>();
            _fakeModelBuilder.Build(default, default).ReturnsForAnyArgs((null, new RegressionModelBuilderOutput()));
            _fakeModelBuilderFactory.CreateFastTreeModelBuilder(default).ReturnsForAnyArgs(_fakeModelBuilder);
        }


        //[Test]
        //public void Create_LoadDataCalled()
        //{
        //    // ARRANGE
        //    ReinitializeFakes();
        //    var service = new FastTreeRegressionService(_fakeModelBuilderFactory,_fakePredictionEngine, _fakeMlDataLoader, _fakeDataPathRegister,new MLContext());

        //    // ACT
        //    service.Create(new CreateFastTreeRegressionModelOptions()
        //    {
        //        TrainingDataName = "file",
        //    });

        //    // ASSERT
        //    _fakeMlDataLoader.Received().LoadData<RegressionPredictionInput>(Arg.Any<string>(), Arg.Any<string>(), "file");
        //}

        [Test]
        public void Create_BuildCalled()
        {
            // ARRANGE
            ReinitializeFakes();
            var service = new FastTreeRegressionService(_fakeModelBuilderFactory, _fakePredictionEngine, _fakeMlDataLoader, new MLContext());

            // ACT
            service.Create(new CreateFastTreeRegressionModelOptions());

            // ASSERT
            _fakeModelBuilder.ReceivedWithAnyArgs().Build(default, default);
        }

        [Test]
        public void Create_SaveModelCalled()
        {
            // ARRANGE
            ReinitializeFakes();
            var service = new FastTreeRegressionService(_fakeModelBuilderFactory, _fakePredictionEngine, _fakeMlDataLoader, new MLContext());

            // ACT
            service.Create(new CreateFastTreeRegressionModelOptions());

            // ASSERT
            _fakeMlDataLoader.ReceivedWithAnyArgs().SaveModel(new SaveMlNetModelDto());
        }

        [Test]
        public void Create_CorrectDataSupplied_ReturnsCorrectData()
        {
            // ARRANGE
            ReinitializeFakes();
            var expectedResult = new RegressionModelBuilderOutput()
            {
                ContributingFeatureIndexes = new List<string> { "Column1", "Column3", "Column9" },
                FeatureImportanceList = new List<RegressionMetricsStatistics>(5),
                ValidationResults = new List<CrossValidationResult<RegressionMetrics>>(10)
      
            };
            var fakeModelBuilderFactory = Substitute.For<IFastTreeModelBuilderFactory>();
            var fakeModelBuilder = Substitute.For<IModelBuilder<FastTreeRegressionModelBuilderOptions>>();
            fakeModelBuilder.Build(default, default).ReturnsForAnyArgs((default, expectedResult));
            fakeModelBuilderFactory.CreateFastTreeModelBuilder(default).ReturnsForAnyArgs(fakeModelBuilder);
            var service = new FastTreeRegressionService(fakeModelBuilderFactory, _fakePredictionEngine, _fakeMlDataLoader, new MLContext());

            // ACT
            var result = service.Create(new CreateFastTreeRegressionModelOptions());

            // ASSERT
            Assert.AreEqual(3, result.ContributingFeatureIndexes.Count());
            Assert.AreEqual(9, result.ContributingFeatureIndexes[2]);
        }


        [Test]
        public void List_ListModelsCalled()
        {
            // ARRANGE
            ReinitializeFakes();
            _fakeMlDataLoader.ListModels(default).ReturnsForAnyArgs(new List<string> { "model1", "model2" });
            var service = new FastTreeRegressionService(_fakeModelBuilderFactory, _fakePredictionEngine, _fakeMlDataLoader, new MLContext());

            // ACT
            service.List();

            // ASSERT
            _fakeMlDataLoader.ReceivedWithAnyArgs().ListModels(default);
        }


        //[Test]
        //public void Delete_SaveModelCalledWithCorrectArguments()
        //{
        //    // ARRANGE
        //    ReinitializeFakes();
        //    var service = new FastTreeRegressionService(_fakeModelBuilderFactory, _fakePredictionEngine, _fakeMlDataLoader, _fakeDataPathRegister, new MLContext());

        //    // ACT
        //    service.Delete("file");

        //    // ASSERT
        //    _fakeDataPathRegister.Received().GetModelFilePath(Arg.Any<string>(), Arg.Any<string>(), Arg.Is<string>(x => x == "file"));
        //}

        [Test]
        public void Delete_PathEmpty_ReturnsFalse()
        {
            // ARRANGE
            ReinitializeFakes();
            _fakeDataPathRegister.GetModelPath(default, default, default).ReturnsForAnyArgs(String.Empty);
            var service = new FastTreeRegressionService(_fakeModelBuilderFactory, _fakePredictionEngine, _fakeMlDataLoader, new MLContext());

            // ACT
            bool result = service.Delete(default);

            // ASSERT
            Assert.AreEqual(false, result);
        }


        //[Test]
        //public void Run_ValidData_LoadModelCalledWithCorrectArguments()
        //{
        //    // ARRANGE
        //    ReinitializeFakes();
        //    _fakePredictionEngine.RunSingle(default, default).ReturnsForAnyArgs((new RegressionPredictionOutput(), new List<string> { "Column1", "Column2" }));
        //    var service = new FastTreeRegressionService(_fakeModelBuilderFactory, _fakePredictionEngine, _fakeMlDataLoader, _fakeDataPathRegister, new MLContext());

        //    // ACT
        //    service.RunSingle("modelName", new string[] { "Column1", "Column2" });

        //    // ASSERT
        //    _fakeMlDataLoader.Received().LoadModel(Arg.Any<string>(), Arg.Any<string>(), Arg.Is<string>(x => x == "modelName"));
        //}

        [Test]
        public void Run_ValidData_RunCalled()
        {
            // ARRANGE
            ReinitializeFakes();
            _fakePredictionEngine.RunSingle(default, default).ReturnsForAnyArgs((new RegressionPredictionOutput(), new List<string> { "Column1", "Column2" }));
            var service = new FastTreeRegressionService(_fakeModelBuilderFactory, _fakePredictionEngine, _fakeMlDataLoader, new MLContext());

            // ACT
            service.RunSingle(default, new string[] { "Column1", "Column2" });

            // ASSERT
            _fakePredictionEngine.Received().RunSingle(Arg.Any<ITransformer>(), Arg.Any<PredictionEngineInput>());
        }

        [Test]
        public void Run_ValidData_ReturnsCorrectResults()
        {
            // ARRANGE
            ReinitializeFakes();
            _fakePredictionEngine.RunSingle(default, default).ReturnsForAnyArgs(
                (new RegressionPredictionOutput { Score = 0.1f, FeatureContributions = new float[] {0.2f, 0.3f }, Features = new float[] { 0.4f, 0.5f } }, 
                new List<string> { "Column1", "Column2" }));


            var service = new FastTreeRegressionService(_fakeModelBuilderFactory, _fakePredictionEngine, _fakeMlDataLoader, new MLContext());

            // ACT
            RegressionServiceRunSingleModelOutput result = service.RunSingle(default, new string[] { "Column1", "Column2" });

            // ASSERT
            Assert.NotNull(result);
            Assert.AreEqual(1, result.ContributingFeatureIndexes[0]);
            Assert.AreEqual(2, result.ContributingFeatureIndexes[1]);
            Assert.AreEqual(0.2f, result.FeatureContributions[0]);
            Assert.AreEqual(0.3f, result.FeatureContributions[1]);
            Assert.AreEqual(0.4f, result.Features[0]);
            Assert.AreEqual(0.5f, result.Features[1]);
        }
   
    }
}
