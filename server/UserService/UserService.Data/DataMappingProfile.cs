using AutoMapper;
using UserService.Data.Entities;
using UserService.Services.Models;

namespace UserService.Data
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<UserModel, User>()
                .ReverseMap();

            CreateMap<AccountModel, Account>()
                .ReverseMap()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(m => m.Balance/100));

            CreateMap<EmailVerificationModel, EmailVerification>()
                .ReverseMap();
        }
    }
}
