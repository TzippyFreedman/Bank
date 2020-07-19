using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.DTO;
using UserService.Services;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<Guid>> LoginAsync([FromQuery]LoginDTO loginDTO)
        {
            Guid AccountId=await _userService.LoginAsync(loginDTO.Email, loginDTO.Password);
            if (AccountId==Guid.Empty)
            {
                
                return Unauthorized();
            }

            return AccountId;

        }


        [HttpGet]
        public async Task<AccountDTO> GetAccountInfo()
        {

            return await _userService.GetAccountInfoAsync();
        }
    }
}