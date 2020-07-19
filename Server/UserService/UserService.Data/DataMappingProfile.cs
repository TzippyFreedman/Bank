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

            CreateMap<Account, AccountModel>()
                .ReverseMap();
           
        }
    }
}
