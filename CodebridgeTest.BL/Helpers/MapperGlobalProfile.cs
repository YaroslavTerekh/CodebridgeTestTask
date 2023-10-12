using AutoMapper;
using CodebridgeTest.Domain.DataTransferObjects;
using CodebridgeTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.BL.Helpers;

public class MapperGlobalProfile : Profile
{
    public MapperGlobalProfile()
    {
        CreateMap<Dog, DogDTO>();
    }
}
