using AutoMapper;
using UserService.Contract.Models;
using UserService.Data.Entities;

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
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(m => m.Balance / 100));

            CreateMap<EmailVerificationModel, EmailVerification>()
                .ReverseMap();

            CreateMap<HistoryOperationModel, HistoryOperation>();
        }
    }
}
