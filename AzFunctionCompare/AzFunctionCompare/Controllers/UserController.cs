using CodeFirstUnitOfWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;

namespace AzFunctionCompare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger log;
        public UserController( ILogger log)
        {
            this.log = log;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> getAll()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(10);
                log.LogInformation("Item " + i);
                list.Add("Item " + i + " - With 10ms thread speed after each item");
            }
            return list;
        }

        [HttpPost]
        public ActionResult<string> AddUser([FromBody] User user)
        {
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
                return BadRequest("Add user failed..");
            }
        }
    }
}
