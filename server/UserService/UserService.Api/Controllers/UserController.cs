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

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult> RegisterAsync(RegisterDTO userRegister)
        {
            UserModel newUserModel = _mapper.Map<UserModel>(userRegister);
            await _userService.RegisterAsync(newUserModel, userRegister.Password, userRegister.VerificationCode);
            return StatusCode((int)HttpStatusCode.Created);
        }

     
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Guid>> LoginAsync([FromQuery] LoginDTO loginDTO)
        {
            Guid accountId = await _userService.LoginAsync(loginDTO.Email, loginDTO.Password);
            return accountId;
        }

       
    }
}