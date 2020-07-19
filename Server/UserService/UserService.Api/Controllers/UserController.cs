using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
       
            bool isRegisterSuccess = await _userService.RegisterAsync(newUserModel);
            return isRegisterSuccess;
        }

        [HttpGet]
        public async Task<ActionResult<Guid>> LoginAsync(LoginDTO loginDTO)
        {
            Guid AccountId = await _userService.LoginAsync(loginDTO.Email, loginDTO.Password);
            if (AccountId == Guid.Empty)
            {

                return Unauthorized();
            }

            return AccountId;

        }
    }
}