using Microsoft.AspNetCore.Mvc;
using ML.Controllers;
using ML.Core;
using ML.Models;
using ML.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace ML.Tests.ML.Controllers
{
    //[TestFixture]
    //public class SdcaControllerTests
    //{
    //    [Test]
    //    public void Create_ValidData_CreateCallReceived()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        var input = new CreateSdcaRegressionRequest
    //        {  
    //            ModelName = "Model",
    //            BiasLearningRate = 0.1f,
    //            ConvergenceTolerance = 0.2f,
    //            MaximumNumberOfIterations = 100,
    //            PermutationCount = 10,
    //            FeatureColumnNames = new string[] {"col1","col2" }, 
    //            LabelColumnName = "col10",
    //            TrainingDataName = "sample.csv",
    //            CrossValidationFoldsCount = 5,
    //            HasFeatureContributionMetrics = true
    //        };

    //        // ACT
    //        controller.Create(input);

    //        // ASSERT
    //        service.Received().Create(Arg.Is<CreateSdcaRegressionModelOptions>(p => p.ModelName == "Model"));
    //        service.Received().Create(Arg.Is<CreateSdcaRegressionModelOptions>(p => p.BiasLearningRate == 0.1f));
    //        service.Received().Create(Arg.Is<CreateSdcaRegressionModelOptions>(p => p.ConvergenceTolerance == 0.2f));
    //        service.Received().Create(Arg.Is<CreateSdcaRegressionModelOptions>(p => p.MaximumNumberOfIterations == 100));
    //        service.Received().Create(Arg.Is<CreateSdcaRegressionModelOptions>(p => p.PermutationCount == 10));
    //        service.Received().Create(Arg.Is<CreateSdcaRegressionModelOptions>(p => p.FeatureColumnNames.ToList().Last() == "col2"));
    //        service.Received().Create(Arg.Is<CreateSdcaRegressionModelOptions>(p => p.LabelColumnName == "col10"));
    //        service.Received().Create(Arg.Is<CreateSdcaRegressionModelOptions>(p => p.TrainingDataName == "sample.csv"));
    //        service.Received().Create(Arg.Is<CreateSdcaRegressionModelOptions>(p => p.CrossValidationFoldsCount == 5));
    //        service.Received().Create(Arg.Is<CreateSdcaRegressionModelOptions>(p => p.HasFeatureContributionMetrics == true));
    //    }


    //    [Test]
    //    public void Create_ParameterNull_ThrowsException()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);

    //        // ACT
    //        // ASSERT
    //        Assert.That(() => controller.Create(null),
    //            Throws.Exception.TypeOf<NullReferenceException>());

    //    }

    //    [Test]
    //    public void Create_AlwaysThrowsException_ThrowsTrainingDataFileNotFoundException()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        service.Create(default).ReturnsForAnyArgs(x => { throw new TrainingDataNotFoundException(); });

    //        // ACT
    //        // ASSERT
    //        Assert.That(() => controller.Create(new CreateSdcaRegressionRequest()),
    //            Throws.Exception.TypeOf<TrainingDataNotFoundException>());

    //    }

    //    [Test]
    //    public void Create_AlwaysThrowsException_ThrowsSavingModelFileException()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        service.Create(default).ReturnsForAnyArgs(x => { throw new SavingModelException(); });

    //        // ACT
    //        // ASSERT
    //        Assert.That(() => controller.Create(new CreateSdcaRegressionRequest()),
    //            Throws.Exception.TypeOf<SavingModelException>());
    //    }

    //    [Test]
    //    public void Create_AlwaysThrowsException_ThrowsException()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        service.Create(default).ReturnsForAnyArgs(x => { throw new Exception(); });

    //        // ACT
    //        // ASSERT
    //        Assert.That(() => controller.Create(new CreateSdcaRegressionRequest()),
    //            Throws.Exception.TypeOf<Exception>());
    //    }

    //    [Test]
    //    public void Delete_CallReceived()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
 
    //        // ACT
    //        controller.Delete("model");

    //        // ASSERT
    //        service.Received().Delete(Arg.Any<string>());
    //    }

    //    [Test]
    //    public void Delete_AlwaysTrue_ReturnsOK()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        service.Delete(default).ReturnsForAnyArgs(true);

    //        // ACT
    //        ObjectResult response = controller.Delete(default) as ObjectResult;

    //        // ASSERT
    //        Assert.IsNotNull(response);
    //        Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
    //    }

    //    [Test]
    //    public void Delete_AlwaysFalse_ReturnsBadRequest()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        service.Delete(default).ReturnsForAnyArgs(false);

    //        // ACT
    //        ObjectResult response = controller.Delete(default) as ObjectResult;

    //        // ASSERT
    //        Assert.IsNotNull(response);
    //        Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
    //    }
      
    //    [Test]
    //    public void RunSinglePrediction_CallReceivedWithCorrectData()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        service.RunSingle(default, default).ReturnsForAnyArgs(new RegressionServiceRunSingleModelOutput{});
    //        var model = new RunSinglePredictionRequestRequest
    //        {
    //            ModelName = "modelName",
    //            Data = new string[]{ "1", "2" }
    //        };
               
    //        // ACT
    //        controller.RunSinglePrediction(model);

    //        // ASSERT
    //        service.Received().RunSingle("modelName", Arg.Is<string[]>(x => x[0] == "1"));
    //        service.Received().RunSingle("modelName", Arg.Is<string[]>(x => x[1] == "2"));
    //    }

    //    [Test]
    //    public void RunSinglePrediction_ValidData_ReturnsOKStatus()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        service.RunSingle( default, default).ReturnsForAnyArgs(new RegressionServiceRunSingleModelOutput
    //        { 
    //            Score = 1.1f, 
    //            Features = new float[] {2.1f, 2.2f },
    //            FeatureContributions = new float[] {0.3f, 0.7f },
    //            ContributingFeatureIndexes = new System.Collections.Generic.List<int> {1,2,3 }
    //        });

    //        // ACT
    //        JsonResult response = controller.RunSinglePrediction(new RunSinglePredictionRequestRequest() { ModelName = "a", Data = new string[2] }) as JsonResult;
    //        var serializedResponse = JsonSerializer.Serialize(response.Value);

    //        // ASSERT
    //        Assert.IsNotNull(response);
    //        Assert.AreEqual("{\"Score\":\"1.1\",\"ContributingFeatureIndexes\":\"[1,2,3]\",\"Features\":\"[2.1,2.2]\",\"FeatureContributions\":\"[0.3,0.7]\"}", serializedResponse);
    //    }
        


    //    [Test]
    //    public void RunSinglePrediction_ThrowsException()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        service.RunSingle(default, default).ReturnsForAnyArgs(x => { throw new Exception(); });

    //        // ACT
    //        // ASSERT
    //        Assert.That(() => controller.RunSinglePrediction(new RunSinglePredictionRequestRequest()),
    //                Throws.Exception.TypeOf<Exception>());
    //    }

    //    [Test]
    //    public void RunSinglePrediction_ThrowsModelLoadingException()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        service.RunSingle(default, default).ReturnsForAnyArgs(x => { throw new ModelLoadingException(); });

    //        // ACT
    //        // ASSERT
    //        Assert.That(() => controller.RunSinglePrediction(new RunSinglePredictionRequestRequest()),
    //                Throws.Exception.TypeOf<ModelLoadingException>());
    //    }

    //    [Test]
    //    public void RunSinglePrediction_ThrowsModelNotFoundException()
    //    {
    //        // ARRANGE
    //        var service = Substitute.For<ISdcaRegressionService>();
    //        var controller = new SdcaController(service);
    //        service.RunSingle(default, default).ReturnsForAnyArgs(x => { throw new ModelNotFoundException(); });

    //        // ACT
    //        // ASSERT
    //        Assert.That(() => controller.RunSinglePrediction(new RunSinglePredictionRequestRequest()),
    //                Throws.Exception.TypeOf<ModelNotFoundException>());
    //    }

    //}
}
