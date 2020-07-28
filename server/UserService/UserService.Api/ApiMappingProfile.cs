using AutoMapper;
using UserService.Api.DTO;
using UserService.Contract.Models;

namespace UserService.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<RegisterDTO, UserModel>();

            CreateMap<UserModel, AccountDTO>();

            CreateMap<EmailVerificationDTO, EmailVerificationModel>();
            CreateMap<HistoryOperationModel, HistoryOperationDTO>()
                                .ForMember(dest => dest.Balance, opt => opt.MapFrom(m => m.Balance  / 100))
                                .ForMember(dest => dest.TransactionAmount, opt => opt.MapFrom(m => m.TransactionAmount / 100));
            CreateMap<PaginationParamsDTO, PaginationParamsModel>();
            CreateMap<PaginationResultModel, PaginationResultDTO>();

        }
    }
}
