using ML.Core;

namespace ML.Services
{
    public class CreateCsvTrainingDataDto : CsvTrainingDataFile
    {
        public byte[] File { get; set; }
    }
}
