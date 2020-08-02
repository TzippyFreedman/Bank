using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Api.DTO;
using UserService.Contract.Models;
using UserService.Services.Interfaces;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(IMapper mapper, IAccountService accountService,IUserService userService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _userService = userService;
        }
        [HttpGet]
        [Route("[action]/{accountId}")]
        public async Task<ActionResult<AccountDTO>> GetAccountDetails(Guid accountId)
        {
            AccountModel account = await _accountService.GetAccountByIdAsync(accountId);
            UserModel user = await _userService.GetUserByIdAsync(account.UserId);
            AccountDTO accountDTO = new AccountDTO();
            _mapper.Map(user, accountDTO);
            accountDTO.Balance = account.Balance;
            accountDTO.OpenDate = account.OpenDate;
            return accountDTO;
        }
    }
}
