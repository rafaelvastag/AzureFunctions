using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunction
{
    public static class AzureFunctionFunctionKeyAccess
    {
        [FunctionName("AzureFunctionFunctionKeyAccess")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "function/{author:alpha}/{id:int?}")] HttpRequest req, string author, int? id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var identify = String.Format($"Author {author}, ID: {id}");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully. Running by: {identify}";

            return new OkObjectResult(responseMessage);
        }
    }
}
