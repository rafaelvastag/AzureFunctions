using AutoMapper;
using AzFunctionsPocs.Models;
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
        private readonly IMapper _mapper;

        public SQLFunctions(PoCContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            List<PoCXpDTO> regsDTO = new List<PoCXpDTO>();
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
                regsDTO = _mapper.Map<List<PoCXpDTO>>(regs);
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            return new OkObjectResult(regsDTO);
        }

        [FunctionName("GetById")]
        public async Task<IActionResult> GetTaskById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "poc/{id}")] HttpRequest req, ILogger log, int id)
        {
            PoCXp res = new PoCXp();
            try
            {
                res = await _context.POC_PARTNER_XP.FirstOrDefaultAsync(p=> p.Id == id);
                log.LogInformation(res.ToString());
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            return new OkObjectResult(_mapper.Map<PoCXpDTO>(res));
        }

        [FunctionName("Delete")]
        public async Task<IActionResult> DeleteTask(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "poc/{id}")] HttpRequest req, ILogger log, int id)
        {
            try
            {
               var res = await _context.POC_PARTNER_XP.FirstOrDefaultAsync(p => p.Id == id);

                if (res != null)
                {
                    _context.POC_PARTNER_XP.Remove(res);
                    await _context.SaveChangesAsync();
                    return new OkObjectResult(true);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
        }

        [FunctionName("Update")]
        public async Task<IActionResult> UpdateTask(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "poc/{id}")] HttpRequest req, ILogger log, int id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<UpdatePoCXp>(requestBody);
            try
            {
                var res = await _context.POC_PARTNER_XP.FirstOrDefaultAsync(p => p.Id == id);

                if (res != null)
                {
                    _context.POC_PARTNER_XP.Update(res);
                    await _context.SaveChangesAsync();
                    return new OkObjectResult(_mapper.Map<PoCXpDTO>(res));
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
        }
    }
}

