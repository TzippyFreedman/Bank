using AutoMapper;
using TransactionService.Api.DTO;
using TransactionService.Contract.Models;

namespace TransactionService.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<TransactionDTO, TransactionModel>()
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(m => m.Amount * 100));
        }
    }
}
