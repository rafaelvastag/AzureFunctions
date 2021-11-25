using Autofac;
using AzureFunction.Models;
using AzureFunction.Models.Interfaces;
using AzureFunctions.Autofac.Configuration;

namespace AzureFunction.Config
{
    public class AutoFacConfig
    {
        public AutoFacConfig(string functionName)
        {
            DependencyInjection.Initialize(builder =>
             {
                 // builder.RegisterType<Dog>();
                 // builder.RegisterType<Cat>().As<IAnimal>();
                 builder.RegisterType<Cat>().Named<IAnimal>("Cat");
                 builder.RegisterType<Dog>().Named<IAnimal>("Dog");
             }, functionName);
        }
    }
}
