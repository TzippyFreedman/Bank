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

        }
    }
}
