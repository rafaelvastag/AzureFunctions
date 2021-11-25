using AzureFunction.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.Models
{
    public class Cat : IAnimal
    {
        public string MakeNoise()
        {
            return "Miau..";
        }
    }
}
