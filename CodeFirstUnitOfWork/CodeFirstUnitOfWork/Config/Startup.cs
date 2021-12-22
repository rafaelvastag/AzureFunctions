using CodeFirstUnitOfWork.DBContext;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CodeFirstUnitOfWork.Config.Startup))]

namespace CodeFirstUnitOfWork.Config
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

            // string connectionString = configuration.GetConnectionString("SQLConnectionString");
            // builder.Services.AddDbContext<BankContext>(
            //   options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
            string connectionString = configuration.GetConnectionString("SQLConnectionAzurePoC");

            builder.Services.AddDbContext<PoCContext>(
               options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
        }
    }
}
