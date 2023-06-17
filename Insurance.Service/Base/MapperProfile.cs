using AutoMapper;
using Insurance.Common.Dto;
using Insurance.Common.Entity;

namespace Insurance.Service.Base;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<BaseDto, BaseEntity>()
            .ForMember(x => x.CreateDate, xx => xx.Ignore())
            .ForMember(x => x.CreateUser, xx => xx.Ignore());

        CreateMap<Person, PersonDto>();
        CreateMap<PersonDto, Person>().IncludeBase<BaseDto, BaseEntity>();

        CreateMap<InsuranceType, InsuranceTypeDto>();
        CreateMap<InsuranceTypeDto, InsuranceType>().IncludeBase<BaseDto, BaseEntity>();

        CreateMap<Request, RequestDto>();
        CreateMap<RequestApiDto, Request>();

        CreateMap<AppUser, AppUserDto>();
        CreateMap<AppUserDto, AppUser>();
    }
}