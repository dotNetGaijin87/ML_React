using ML.Core;

namespace ML.Core
{
    /// <summary>
    /// A csv training data model
    /// </summary>
    public class CsvTrainingDataModel: TrainingDataModel
    {
        public CsvTrainingDataModel()
        {
            Extension = "csv";
        }
    }
}
