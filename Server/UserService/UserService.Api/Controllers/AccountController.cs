using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.DTO;
using UserService.Data.Entities;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       // private readonly IAccountService _accountService;

       // public AccountController(IAccountService userService,IMapper _mapper)
       // {
       //     _accountService = userService;
       // }

       // [HttpGet]
       //public  Task<AccountDTO> Get(AccountDTO accountDTO)
       // {
       //     _mapper.Map<AccountModel>(accountModel);
       //     _accountService.Get()
       // }
    }
}