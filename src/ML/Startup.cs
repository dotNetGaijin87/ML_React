using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ML;
using ML.AiModels;
using ML.AiModels.Forecasting;
using ML.AiModels.Regression;
using ML.Common.DataPathRegister;
using ML.Common.MlRepository;
using ML.Services;
using ML.Services.Environment;
using ML.Services.Forecasting;
using ML.Services.Regression;
using System.IO;

namespace ML
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            // Uniformly handles exeptions in the application
            // by sending problem details to the client.
            // In case of model validation errors ValidationProblemDetails is sent
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) =>
                {
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() || env.IsStaging();
                };
            });

            // common
            services.AddScoped( provider => new MLContext(seed: 2020));
            services.AddScoped<ITrainingDataService, TrainingDataService>();
            services.AddScoped<IMlRepository, MlRepository>();
            services.AddScoped<IInfoService, InfoService>();
            services.AddSingleton(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                IDataPathRegister dataPathRegister = new DataPathRegister(
                                                        Path.Combine(env.ContentRootPath, "TrainingData"),
                                                        Path.Combine(env.ContentRootPath, "AIModels"));
                return dataPathRegister;
            });


            // regression
            services.AddSingleton<IFastForestModelBuilderFactory, RegressionModelFactory>();
            services.AddSingleton<ISdcaModelBuilderFactory, RegressionModelFactory>();
            services.AddSingleton<IFastTreeModelBuilderFactory, RegressionModelFactory>();
            services.AddSingleton<IOnlineGradientDescentModelBuilderFactory, RegressionModelFactory>();
            services.AddSingleton<IGamModelBuilderFactory, RegressionModelFactory>();
            services.AddSingleton<IFastTreeTweedieModelBuilderFactory, RegressionModelFactory>();
            services.AddSingleton<ILbfgsPoissonRegressionModelBuilderFactory, RegressionModelFactory>();
            services.AddScoped<IRegressionPredictionEngine, RegressionPredictionEngine>();
            services.AddScoped<IRegressionServiceBase, RegressionServiceBase>();
            services.AddScoped<IFastForestRegressionService, FastForestRegressionService>();
            services.AddScoped<IFastTreeRegressionService, FastTreeRegressionService>();
            services.AddScoped<ISdcaRegressionService, SdcaRegressionService>();

            // anomaly detection
            services.AddScoped<ISrCnnTrainer, SrCnnTrainer>();
            services.AddScoped<ISrCnnService, SrCnnService>();

            // forecasting
            services.AddScoped<ISsaForecastingModelBuilder, SsaForecastingModelBuilder>();
            services.AddScoped<ISsaForecastingService, SsaForecastingService>();   
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
