using AutoMapper;
using TransactionService.Contract.Models;
using TransactionService.Data.Entities;

namespace TransactionService.Data
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<TransactionModel, Transaction>()
                .ReverseMap()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(m => m.Amount / 100));
        }
    }
}
