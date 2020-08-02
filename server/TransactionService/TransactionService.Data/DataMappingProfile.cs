using AutoMapper;
using TransactionService.Contract.Models;
using TransactionService.Data.Entities;

namespace TransactionService.Data
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<Transaction, TransactionModel>()
              .ReverseMap();
        }
    }
}
