using CodeFirstUnitOfWork.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CodeFirstUnitOfWork.AccountAdder
{
    public class AccountAdder
    {

       // private readonly BankContext _context;

        public AccountAdder()
        {
            // _context = context;
        }

        [FunctionName("AccountAdder")]
        public async Task<string> RegisterAccount(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "account")] HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("Received new account register request");

            try
            {
                var user = await req.Content.ReadAsAsync<User>();
                log.LogInformation("User: " + user.FirstName);

                if (null != user)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        log.LogInformation("Calculating.. " + i);
                    }
                    log.LogInformation("Saving new User..");

                    Thread.Sleep(100);
                    // _context.Users.Add(user);
                    //await _context.SaveChangesAsync();
                    return "POST NEW ITEM - WITH 100ms THREAD SLEEP FOR USER: " + user.FirstName + " " + user.LastName;
                }
                else
                {
                    log.LogError("Failed to parse user received");
                    return "Failed to parse user received";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [FunctionName("AccountGet")]
        public List<string> getAccounts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "account")] HttpRequestMessage req, ILogger log)
        {
           log.LogInformation("Received new account register request");

            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i < 100; i++)
                {
                        Thread.Sleep(10);
                        log.LogInformation("Item " + i);
                        list.Add("Item " + i + " - With 10ms thread speed after each item");
                }
                log.LogInformation(list.ToString());

                return list;
            }
            catch (Exception e)
            {
                throw e; 
            }
        }
    }
}
