using CodeFirstUnitOfWork.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeFirstUnitOfWork.AccountAdder
{
    public class AccountAdder
    {

        private readonly BankContext _context;

        public AccountAdder(BankContext context)
        {
            _context = context;
        }

        [FunctionName("AccountAdder")]
        public async Task<HttpResponseMessage> RegisterAccount(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("Received new account register request");

            try
            {
                var user = await req.Content.ReadAsAsync<User>();

                if (null != user)
                {
                    log.LogInformation("Saving new User..");


                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    log.LogError("Failed to parse user received");
                    return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to parse user received");
                }
            }
            catch (Exception e)
            {

                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
