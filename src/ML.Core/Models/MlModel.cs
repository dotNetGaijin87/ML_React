namespace ML.Core
{
    /// <summary>
    /// A Machine Learning model entity
    /// </summary>
    public class MlModel : Entity
    {
        public MlModel()
        {
            EntityType = EntityType.AiModel;
        }

        public string Name { get; init; }
        public string Extension { get; init; }
    }
}
