using AutoMapper;
using MediUp.Domain.Dtos;
using MediUp.Domain.Entities;

namespace MediUp.Application.Mapping;

public class ElectriCompanyProfile : Profile
{
    public ElectriCompanyProfile()
    {
        CreateMap<CreateElectriCompanyRequest, ElectriCompany>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()))
            .ForMember(dest => dest.TaxId, opt => opt.MapFrom(src => src.TaxId.Trim()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.ContactEmail))
            .ForMember(dest => dest.ContactPhone, opt => opt.MapFrom(src => src.ContactPhone))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
    }
}
