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
            CreateMap<AddressModel, Address>()
                .ReverseMap();

            CreateMap<AccountModel, Account>()
                .ReverseMap();

            CreateMap<EmailVerificationModel, EmailVerification>()
                .ReverseMap();

            CreateMap<SucceededOperationModel, SucceededOperation>()
                .ReverseMap();

            CreateMap<FailedOperationModel, FailedOperation>();

        }
    }
}
