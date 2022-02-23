using Microsoft.AspNetCore.Http;
using ML.Controllers;
using ML.Controllers.TrainingData;
using ML.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ML.Tests.ML.Controllers
{
    [TestFixture]
    public class TrainingDataControllerTests
    {
        [Test]
        public async Task Create_ThrowsException_ReturnsFalse()
        {
            // ARRANGE
            ITrainingDataService trainingDataService = Substitute.For<ITrainingDataService>();
            TrainingDataController trainingDataController = new TrainingDataController(trainingDataService);
            trainingDataService.Create(default).Returns(Task.FromException<bool>(new Exception("")));
            IFormFile formFile = Substitute.For<IFormFile>();
  

            var request = new UploadTrainingDataRequest()
            {
                 FormFile = formFile
            };

            // ACT
            bool response = (await trainingDataController.Create(request));
            
            // ASSERT
            Assert.IsFalse(response);
        }

        [Test]
        public async Task Create_ValidData_ReturnsTrue()
        {
            // ARRANGE
            ITrainingDataService trainingDataService = Substitute.For<ITrainingDataService>();
            TrainingDataController trainingDataController = new TrainingDataController(trainingDataService);
            trainingDataService.Create(default).ReturnsForAnyArgs(true);
            IFormFile formFile = Substitute.For<IFormFile>();


            var request = new UploadTrainingDataRequest()
            {
                FormFile = formFile
            };

            // ACT
            bool response = (await trainingDataController.Create(request));

            // ASSERT
            Assert.IsTrue(response);
        }

    }
}
