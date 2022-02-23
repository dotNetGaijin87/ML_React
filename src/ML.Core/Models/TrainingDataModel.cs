using ML.Core;

namespace ML.Core
{
    /// <summary>
    /// A trainig data model entity
    /// </summary>
    public class TrainingDataModel: Entity
    {
        public TrainingDataModel()
        {
            EntityType = EntityType.TrainingData;
        }
        public string Name { get; set; }
        public string Extension { get; init; }
    }
}
