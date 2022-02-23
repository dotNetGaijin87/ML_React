using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.AiModels.Regression
{
    public interface ILbfgsPoissonRegressionModelBuilderFactory
    {
        public IModelBuilder<SdcaRegressionModelBuilderOptions> CreateLbfgsPoissonModelBuilder(MLContext mlContext);

    }
}
