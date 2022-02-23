namespace ML.Services
{
    public class CsvTrainingDataFile
    {
        public string ModelCategoryName { get; set; }
        public string ModelTypeName { get; set; }
        public string FileName { get; set; }
        public string Extension { get; } = "csv";
    }
}
