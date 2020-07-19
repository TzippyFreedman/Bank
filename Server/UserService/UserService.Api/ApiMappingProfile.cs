using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Api.DTO;
using UserService.Services.Models;

namespace UserService.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<RegisterDTO, UserModel>();
            CreateMap<AccountModel, AccountDTO>();
        }
    }
}
