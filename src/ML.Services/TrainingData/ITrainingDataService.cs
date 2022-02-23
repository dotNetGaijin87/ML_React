using ML.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML.Services
{
    public interface ITrainingDataService
    {
        Task<bool> Create(CreateCsvTrainingDataDto trainingData);
        bool Delete(CsvTrainingDataFile trainingData);
        string[] GetHeaders(CsvTrainingDataFile trainingData);
        IEnumerable<string> List(CsvTrainingDataFile trainingData);
    }
}