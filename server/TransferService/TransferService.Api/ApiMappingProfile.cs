using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransferService.Api.DTO;
using TransferService.Contract.Models;

namespace TransferService.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<TransferDTO, TransferModel>()
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(m => m.Amount * 100));

        }
    }
}
