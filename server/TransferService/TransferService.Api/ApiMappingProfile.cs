using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransferService.Api.DTO;
using TransferService.Services.Models;

namespace TransferService.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<TransferDTO, TransferModel>();
        }
    }
}
