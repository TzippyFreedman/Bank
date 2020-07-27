using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TransferService.Contract.Models;
using TransferService.Data.Entities;

namespace TransferService.Data
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<TransferModel, Transfer>()
                .ReverseMap()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(m => m.Amount / 100));
        }
    }
}
