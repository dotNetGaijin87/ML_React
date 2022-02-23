using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.AiModels.Regression
{
    public interface IFastTreeTweedieModelBuilderFactory
    {
        public IModelBuilder<SdcaRegressionModelBuilderOptions> CreateFastTreeTweedieModelBuilder(MLContext mlContext);
    }
}
