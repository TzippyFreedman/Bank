using AutoMapper;
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
