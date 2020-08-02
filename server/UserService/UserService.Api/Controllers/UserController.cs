using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using UserService.Api.DTO;
using UserService.Contract.Models;
using UserService.Services.Interfaces;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public UserController(IMapper mapper, IUserService userService, IAccountService accountService)
        {
            _mapper = mapper;
            _userService = userService;
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<ActionResult> RegisterAsync(RegisterDTO userRegister)
        {
            UserModel newUserModel = _mapper.Map<UserModel>(userRegister);
            await _userService.RegisterAsync(newUserModel, userRegister.Password, userRegister.VerificationCode);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task VerifyEmailAsync([FromBody] EmailVerificationDTO emailVerificationDTO)
        {
            EmailVerificationModel emailVerification = _mapper.Map<EmailVerificationModel>(emailVerificationDTO);
            await _userService.VerifyEmailAsync(emailVerification);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Guid>> LoginAsync([FromQuery] LoginDTO loginDTO)
        {
            Guid accountId = await _userService.LoginAsync(loginDTO.Email, loginDTO.Password);
            return accountId;
        }

        [HttpGet]
        [Route("[action]/{accountId}")]
        public async Task<ActionResult<AccountDTO>> GetAccountDetails(Guid accountId)
        {
            AccountModel account = await _accountService.GetByIdAsync(accountId);
            UserModel user = await _userService.GetByIdAsync(account.UserId);
            AccountDTO accountDTO = new AccountDTO();
            _mapper.Map(user, accountDTO);
            accountDTO.Balance = account.Balance;
            accountDTO.OpenDate = account.OpenDate;
            return accountDTO;
        }
    }
}