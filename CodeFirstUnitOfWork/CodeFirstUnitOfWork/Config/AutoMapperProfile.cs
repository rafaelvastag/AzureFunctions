using AutoMapper;
using AzFunctionsPocs.Models;
using CodeFirstUnitOfWork.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzFunctionsPocs.Config
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            this.CreateMap<PoCXp, PoCXpDTO>().ReverseMap();
        }
    }
}
