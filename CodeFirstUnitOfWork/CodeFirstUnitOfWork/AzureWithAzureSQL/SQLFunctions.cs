using CodeFirstUnitOfWork.DBContext;
using CodeFirstUnitOfWork.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CodeFirstUnitOfWork.AzureWithAzureSQL
{
    public class SQLFunctions
    {

        private readonly PoCContext _context;

        public SQLFunctions(PoCContext context)
        {
            _context = context;
        }

        [FunctionName("Create")]
        public static async Task<IActionResult> CreateTask(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "poc")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<CreatePoCXp>(requestBody);
            try
            {
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
            return new OkResult();
        }

        [FunctionName("Get")]
        public async Task<IActionResult> GetTasks(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "poc")] HttpRequest req, ILogger log)
        {
            List<PoCXp> regs = new List<PoCXp>();
            try
            {
                regs = await _context.POC_PARTNER_XP.ToListAsync();
                log.LogInformation(regs.ToArray().ToString());
                //using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("sqldb_connection")))
                //{
                //    connection.Open();
                //    var query = @"Select * from [POC_PARTNER_XP]";
                //    SqlCommand command = new SqlCommand(query, connection);
                //    var reader = await command.ExecuteReaderAsync();
                //    while (reader.Read())
                //    {
                //        PoCXp reg = new PoCXp()
                //        {
                //            Id = (int)reader["Id"],
                //            Description = reader["Description"].ToString(),
                //            CreatedOn = (DateTime)reader["CreatedOn"],
                //            Updated = (bool)reader["Updated"]
                //        };
                //        regs.Add(reg);
                //    }
                //}
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            return new OkObjectResult(regs);
        }

        [FunctionName("GetById")]
        public static IActionResult GetTaskById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "poc/{id}")] HttpRequest req, ILogger log, int id)
        {
            try
            {

            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            return new OkObjectResult("ok");
        }

        [FunctionName("Delete")]
        public static IActionResult DeleteTask(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "poc/{id}")] HttpRequest req, ILogger log, int id)
        {
            try
            {

            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
            return new OkResult();
        }

        [FunctionName("Update")]
        public static async Task<IActionResult> UpdateTask(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "poc/{id}")] HttpRequest req, ILogger log, int id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<UpdatePoCXp>(requestBody);
            try
            {

            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            return new OkResult();
        }
    }
}

