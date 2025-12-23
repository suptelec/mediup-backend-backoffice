using AutoMapper;
using MediUp.Domain.Dtos;
using MediUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Application;
public class MappingProfile : Profile
{
    /// <summary>
    /// Constructor que configura los mapeos entre entidades y DTOs
    /// </summary>
    public MappingProfile()
    {
        CreateMap<CreateElectriCompanyRequest, ElectriCompany>().ReverseMap();
        CreateMap<ElectriCompany, ElectriCompanyResponse>().ReverseMap();
    }

}
