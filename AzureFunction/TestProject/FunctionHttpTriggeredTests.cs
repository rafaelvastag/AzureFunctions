using AzureFunction.Functions;
using AzureFunction.Models;
using AzureFunction.Models.Interfaces;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class FunctionHttpTriggeredTests
    {
        [Fact]
        public async Task ShoudReturnCatNoiseAsync()
        {
            HttpRequestMessage request = TestHelpers.CreateGetRequest();
            TraceWriterStub traceWriter = new TraceWriterStub(System.Diagnostics.TraceLevel.Info);
            var response = await DogDependencyInjectionFunction.IAnimalDependencyTesting(request, traceWriter, new Cat());
            
            Assert.Equal("Your Animal make: Miau..", response.Value.Value.ToString());
        }

        [Fact]
        public async Task ShoudReturnDogNoiseAsync()
        {
            HttpRequestMessage request = TestHelpers.CreateGetRequest();
            TraceWriterStub traceWriter = new TraceWriterStub(System.Diagnostics.TraceLevel.Info);
            var response = await DogDependencyInjectionFunction.IAnimalDependencyTesting(request, traceWriter, new Dog());

            Assert.Equal("Your Animal make: Au Au...", response.Value.Value.ToString());
        }

        [Fact]
        public async Task ShoudReturnMooAsync()
        {
            HttpRequestMessage request = TestHelpers.CreateGetRequest();
            TraceWriterStub traceWriter = new TraceWriterStub(System.Diagnostics.TraceLevel.Info);
            Mock<IAnimal> cow = new Mock<IAnimal>();
            cow.Setup(x => x.MakeNoise()).Returns("Moo...");
            var response = await DogDependencyInjectionFunction.IAnimalDependencyTesting(request, traceWriter, cow.Object);

            Assert.Equal("Your Animal make: Moo...", response.Value.Value.ToString());
        }
    }
}
