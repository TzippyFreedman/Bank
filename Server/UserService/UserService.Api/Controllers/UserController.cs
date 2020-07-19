using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.DTO;
using UserService.Api.Exceptions;
using UserService.Services;
using UserService.Services.Models;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> RegisterAsync(RegisterDTO userRegister)
        {
            UserModel newUserModel = _mapper.Map<UserModel>(userRegister);
       
            bool isRegisterSuccess = await _userService.RegisterAsync(newUserModel);
            return isRegisterSuccess;
        }

        [HttpGet]
        [Route("[action]")]


        public async Task<ActionResult<Guid>> LoginAsync([FromQuery] LoginDTO loginDTO)
        {
            Guid AccountId = await _userService.LoginAsync(loginDTO.Email, loginDTO.Password);
            if (AccountId == Guid.Empty)
            {

                return Unauthorized();
            }

            return AccountId;

        }
        [HttpGet]
        [Route("[action]/{accountId}")]

        public async Task<ActionResult<AccountDTO>> GetAccountDetails(Guid accountId)
        {
            AccountModel account=await  _userService.GetAccountDetailsAsync(accountId);
            if (account == null)
            {
                throw new AccountNotFoundException(accountId);
            }
            UserModel user = await _userService.GetUserByIdAsync(account.UserId);
            return new AccountDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Balance = account.Balance,
                OpenDate = account.OpenDate,
                Email = user.Email
            };
          
        }
    }
}