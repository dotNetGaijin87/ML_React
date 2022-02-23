using Microsoft.AspNetCore.Http;
using ML.Common.DataPathRegister;
using ML.Common.MlRepository;
using ML.Core;
using ML.Services;
using NSubstitute;
using NUnit.Framework;

namespace ML.Tests.ML.Services.TrainingDataInput
{
    [TestFixture]
    public class TrainingDataServiceTests
    {
        IDataPathRegister _dataPathRegister;
        IMlRepository _mlRepository;
        IFormFile _formFile;
        ITrainingDataService _trainingDataService;

        [SetUp]
        public void Setup()
        {
            _dataPathRegister = Substitute.For<IDataPathRegister>();
            _mlRepository = Substitute.For<IMlRepository>();
            _formFile = Substitute.For<IFormFile>();
            _trainingDataService = new TrainingDataService(_dataPathRegister, _mlRepository);
        }



        [Test]
        public void Create_ValidData_SaveTrainingDataCalledWithCorrectArguments()
        {
            // ARRANGE
            _formFile.FileName.Returns("File1");
            var model = new CreateCsvTrainingDataDto
            {
                ModelCategoryName = MlCategory.Forecasting.ToString(),
                ModelTypeName = MlAlgorithm.FastForest.ToString(),
                FileName = "File1Name",
                File = new byte[2]
            };

            // ACT
            _trainingDataService.Create(model);

            // ASSERT
            _mlRepository.Received().SaveTrainingData(Arg.Is<SaveTrainingData>(x => x.Category == MlCategory.Forecasting));
            _mlRepository.Received().SaveTrainingData(Arg.Is<SaveTrainingData>(x => x.Type == MlAlgorithm.FastForest));
            _mlRepository.Received().SaveTrainingData(Arg.Is<SaveTrainingData>(x => x.Name == "File1Name"));
        }


        [Test]
        public void Delete_ValidData_GetTrainingDataFilePathCalled()
        {
            // ARRANGE
            // ACT
            var model = new CsvTrainingDataFile
            {
                ModelCategoryName = $"{MlCategory.Forecasting.ToString()}",
                ModelTypeName = $"{MlAlgorithm.FastForest.ToString()}",
                FileName = "model"
            };

            _trainingDataService.Delete(model);

            // ASSERT
            _mlRepository.Received().DeleteTrainingData(Arg.Is<TrainingDataModel>(x => x.Category == MlCategory.Forecasting));
            _mlRepository.Received().DeleteTrainingData(Arg.Is<TrainingDataModel>(x => x.AlgorithmType == MlAlgorithm.FastForest));
            _mlRepository.Received().DeleteTrainingData(Arg.Is<TrainingDataModel>(x => x.Name == "model"));
        }
    }
}
