namespace ML.Core
{
    /// <summary>
    /// A base entity for all ml domain models
    /// </summary>
    public class Entity
    {    
        public EntityType EntityType { get; set; }
        public MlCategory Category { get; set; }
        public MlAlgorithm AlgorithmType { get; set; }
    }
}
