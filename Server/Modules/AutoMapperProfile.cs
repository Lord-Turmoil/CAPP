using AutoMapper;
using Server.Modules.Core.Dtos;
using Server.Modules.Core.Models;
using Shared.Dtos;

namespace Server.Modules;

public class AutoMapperProfile : MapperConfigurationExpression
{
    public AutoMapperProfile()
    {
        CreateMap<Part, PartDto>().ReverseMap();
        CreateMap<Procedure, ProcedureDto>().ReverseMap();
        CreateMap<Group, GroupDto>().ReverseMap();
    }
}