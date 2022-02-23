using ML.Controllers;
using ML.Core;
using ML.Services.Regression;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.Tests.ML.Controllers
{
    [TestFixture]
    public class FastForestControllerTests
    {
        [Test]
        public void Create_ValidData_CreateCallReceived()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            var input = new CreateFastForestRegressionDto
            {  
                ModelName = "Model", 
                TreesCount = 100,
                LeavesCount = 10,
                MinimumExampleCountPerLeaf = 5,
                FeatureColumnNames = new string[] {"col1","col2" }, 
                LabelColumnName = "col10",
                TrainingDataName = "sample.csv",
                CrossValidationFoldsCount = 5,
                HasFeatureContributionMetrics = true
            };

            // ACT
            controller.Create(input);

            // ASSERT
            service.Received().Create(Arg.Is<CreateFastForestRegressionDto>(p => p.ModelName == "Model"));
            service.Received().Create(Arg.Is<CreateFastForestRegressionDto>(p => p.TreesCount == 100));
            service.Received().Create(Arg.Is<CreateFastForestRegressionDto>(p => p.LeavesCount == 10));
            service.Received().Create(Arg.Is<CreateFastForestRegressionDto>(p => p.MinimumExampleCountPerLeaf == 5));
            service.Received().Create(Arg.Is<CreateFastForestRegressionDto>(p => p.FeatureColumnNames.ToList().Last() == "col2"));
            service.Received().Create(Arg.Is<CreateFastForestRegressionDto>(p => p.LabelColumnName == "col10"));
            service.Received().Create(Arg.Is<CreateFastForestRegressionDto>(p => p.TrainingDataName == "sample.csv"));
            service.Received().Create(Arg.Is<CreateFastForestRegressionDto>(p => p.CrossValidationFoldsCount == 5));
            service.Received().Create(Arg.Is<CreateFastForestRegressionDto>(p => p.HasFeatureContributionMetrics == true));
        }

        [Test]
        public void Create_AlwaysThrowsException_ThrowsTrainingDataFileNotFoundException()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            service.Create(default).ReturnsForAnyArgs(x => { throw new TrainingDataNotFoundException(); });

            // ACT
            // ASSERT
            Assert.That(() => controller.Create(new CreateFastForestRegressionDto()),
                Throws.Exception.TypeOf<TrainingDataNotFoundException>());

        }

        [Test]
        public void Create_AlwaysThrowsException_ThrowsSavingModelFileException()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            service.Create(default).ReturnsForAnyArgs(x => { throw new SavingModelException(); });

            // ACT
            // ASSERT
            Assert.That(() => controller.Create(new CreateFastForestRegressionDto()),
                Throws.Exception.TypeOf<SavingModelException>());
        }

        [Test]
        public void Create_AlwaysThrowsException_ThrowsException()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            service.Create(default).ReturnsForAnyArgs(x => { throw new Exception(); });

            // ACT
            // ASSERT
            Assert.That(() => controller.Create(new CreateFastForestRegressionDto()),
                Throws.Exception.TypeOf<Exception>());
        }

        [Test]
        public void Delete_CallReceived()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
 
            // ACT
            controller.Delete("model");

            // ASSERT
            service.Received().Delete("model");
        }

        [Test]
        public void Delete_AlwaysTrue_ReturnsOK()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            service.Delete(default).ReturnsForAnyArgs(true);

            // ACT
            bool response = controller.Delete(default);

            // ASSERT
            Assert.IsTrue(response);
        }

        [Test]
        public void Delete_AlwaysFalse_ReturnsBadRequest()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            service.Delete(default).ReturnsForAnyArgs(false);

            // ACT
            bool response = controller.Delete(default);

            // ASSERT
            Assert.IsFalse(response);
        }
      
        [Test]
        public void RunSinglePrediction_CallReceivedWithCorrectData()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            service.RunSingle(default, default).ReturnsForAnyArgs(new RegressionServiceRunSingleModelOutput{});
            var model = new RunSinglePredictionDto
            {
                ModelName = "modelName",
                Data = new string[]{ "1", "2" }
            };
               
            // ACT
            controller.RunSinglePrediction(model);

            // ASSERT
            service.Received().RunSingle(Arg.Is<string>(x => x == "modelName"), Arg.Is<string[]>(x => x[0] == "1"));
            service.Received().RunSingle(Arg.Is<string>(x => x == "modelName"), Arg.Is<string[]>(x => x[1] == "2"));
        }

        [Test]
        public void RunSinglePrediction_ValidData_ReturnsOKStatus()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            service.RunSingle(default, default).ReturnsForAnyArgs(new RegressionServiceRunSingleModelOutput
            { 
                Score = 1.1f, 
                Features = new float[] {2.1f, 2.2f },
                FeatureContributions = new float[] {0.3f, 0.7f },
                ContributingFeatureIndexes = new List<int> {1,2,3 }
            });

            // ACT
            var response = controller.RunSinglePrediction(new RunSinglePredictionDto() { ModelName = "a", Data = new string[2] });
        

            // ASSERT
            Assert.IsNotNull(response);
            Assert.AreEqual(response.ContributingFeatureIndexes.Count(), 3);
            Assert.AreEqual(response.FeatureContributions.Count(), 2);
            Assert.AreEqual(response.Features.Count(), 2);
            Assert.Greater(response.Score, 1.0);


        }

        [Test]
        public void RunSinglePrediction_ThrowsException()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            service.RunSingle(default, default).ReturnsForAnyArgs(x => { throw new Exception(); });

            // ACT
            // ASSERT
            Assert.That(() => controller.RunSinglePrediction(new RunSinglePredictionDto()),
                    Throws.Exception.TypeOf<Exception>());
        }

        [Test]
        public void RunSinglePrediction_ThrowsModelLoadingException()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            service.RunSingle(default, default).ReturnsForAnyArgs(x => { throw new ModelLoadingException(); });

            // ACT
            // ASSERT
            Assert.That(() => controller.RunSinglePrediction(new RunSinglePredictionDto()),
                    Throws.Exception.TypeOf<ModelLoadingException>());
        }

        [Test]
        public void RunSinglePrediction_ThrowsModelNotFoundException()
        {
            // ARRANGE
            var service = Substitute.For<IFastForestRegressionService>();
            var controller = new FastForestController(service);
            service.RunSingle(default, default).ReturnsForAnyArgs(x => { throw new ModelNotFoundException(); });

            // ACT
            // ASSERT
            Assert.That(() => controller.RunSinglePrediction(new RunSinglePredictionDto()),
                    Throws.Exception.TypeOf<ModelNotFoundException>());
        }


        [Test]
        public void RunMultiplePredictions_CallReceivedWithCorrectData()
        {
            // ARRANGE
            var service = Substitute.For<ISdcaRegressionService>();
            var controller = new SdcaController(service);
            service.RunMultiple(default, default).ReturnsForAnyArgs(
                new RegressionServiceRunMultipleModelOutput 
                { 
                    Scores = new List<float>()
                });
            var model = new RunMultiplePredictionsRequest
            {
                ModelName = "modelName",
                Data = new string[3][]{
                               new string[2] {"1", "2"},
                               new string[2] {"3", "4"},
                               new string[2] {"5", "6"}
                            }
            };

            // ACT
            controller.RunMultiplePredictions(model);

            // ASSERT
            service.Received().RunMultiple("modelName", Arg.Is<string[][]>(x => x[0][0] == "1"));
            service.Received().RunMultiple("modelName", Arg.Is<string[][]>(x => x[2][1] == "6"));
        }
    }
}
