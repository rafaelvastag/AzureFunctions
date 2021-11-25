using AzureFunction.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.Models
{
    public class Dog : IAnimal
    {
        public string Name { get; set; }

        public string MakeNoise()
        {
            return "Au Au";
        }
    }
}
