using AzureFunction.Config;
using AzureFunction.Models;
using AzureFunction.Models.Interfaces;
using AzureFunctions.Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace AzureFunction.Functions
{
    [DependencyInjectionConfig(typeof(AutoFacConfig))]
    public static class DogDependencyInjectionFunction
    {
      
        [FunctionName("IAnimalDependencyInjection")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "animal")] HttpRequest req,
        ILogger log, [Inject] IAnimal animal)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = $"Your Animal make: {animal.MakeNoise()}";

            return new OkObjectResult(responseMessage);
        }
    }
}
