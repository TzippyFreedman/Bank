using AutoMapper;
using System;
using UserService.Api.DTO;
using UserService.Services.Models;

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
