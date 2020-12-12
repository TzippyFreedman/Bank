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
            CreateMap<UserDTO, UserModel>()
                .ReverseMap();

            CreateMap<AddressDTO, AddressModel> ()
                   .ReverseMap();

            CreateMap<AccountModel, AccountDTO>()
                .ForMember(dest => dest.Income, opt => opt.MapFrom(m => (float)m.Balance / 100));

            CreateMap<EmailVerificationDTO, EmailVerificationModel>();

            CreateMap<SucceededOperationModel, SucceededOperationDTO>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(m => (float)m.Balance  / 100  ))
                .ForMember(dest => dest.TransactionAmount, opt => opt.MapFrom(m => (float)m.TransactionAmount / 100));

            CreateMap<PaginationParamsDTO, PaginationParamsModel>();

            CreateMap<PaginationResultModel, PaginationResultDTO>();

        }
    }
}
