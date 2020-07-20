using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserService.Api.DTO;
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
            await _userService.RegisterAsync(newUserModel, userRegister.Password, userRegister.VerificationCode);
            return StatusCode(201);
            //return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);

        }

        [HttpPost]
        [Route("[action]")]
        public async Task VerifyEmailAsync([FromBody] EmailVerificationDTO emailVerificationDTO)
        {
            EmailVerificationModel emailVerification= _mapper.Map<EmailVerificationModel>(emailVerificationDTO);
             await _userService.VerifyEmailAsync(emailVerification);
        }

        //fix if empty
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Guid>> LoginAsync([FromQuery] LoginDTO loginDTO)
        {
            Guid accountId = await _userService.LoginAsync(loginDTO.Email, loginDTO.Password);
            if (accountId == Guid.Empty)
            {
                return Unauthorized();
            }
            return accountId;
        }

        [HttpGet]
        [Route("[action]/{accountId}")]
        public async Task<ActionResult<AccountDTO>> GetAccountDetails(Guid accountId)
        {
            AccountModel account = await _userService.GetAccountByIdAsync(accountId);
            UserModel user = await _userService.GetUserByIdAsync(account.UserId);
            AccountDTO accountDTO = new AccountDTO();
            _mapper.Map(user, accountDTO);
            accountDTO.Balance = account.Balance;
            accountDTO.OpenDate = account.OpenDate;
            return accountDTO;
        }
    }
}