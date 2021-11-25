using AzureFunction.Config;
using AzureFunction.Models;
using AzureFunction.Models.Interfaces;
using AzureFunctions.Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureFunction.Functions
{
    [DependencyInjectionConfig(typeof(AutoFacConfig))]
    public static class DogDependencyInjectionFunction
    {
      
        [FunctionName("IAnimalDependencyInjection")]
        public static async Task<IActionResult> IAnimalDependency(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "animal")] HttpRequest req,
        ILogger log, [Inject] IAnimal animal)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = $"Your Animal make: {animal.MakeNoise()}";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("IAnimalDependencyInjectionTesting")]
        public static async Task<ActionResult<ObjectResult>> IAnimalDependencyTesting(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "animal")] HttpRequestMessage req,
        TraceWriter log, [Inject] IAnimal animal)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string responseMessage = $"Your Animal make: {animal.MakeNoise()}";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("IAnimalNamedDependencyInjection")]
        public static async Task<IActionResult> IAnimalNamedDependency(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "animalNamed")] HttpRequest req,
        ILogger log, [Inject("Cat")] IAnimal cat, [Inject("Dog")] IAnimal dog)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = $"Your Cat make: {cat.MakeNoise()} and your dog make {dog.MakeNoise()}";

            return new OkObjectResult(responseMessage);
        }
    }
}
