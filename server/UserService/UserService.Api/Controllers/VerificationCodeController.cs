using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserService.Api.DTO;
using UserService.Contract.Models;
using UserService.Services.Interfaces;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationCodeController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public VerificationCodeController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Route("[action]")]
        //check neccesity of from body
        public async Task VerifyEmailAsync(EmailVerificationDTO emailVerificationDTO)
        {
            EmailVerificationModel emailVerification = _mapper.Map<EmailVerificationModel>(emailVerificationDTO);
            await _userService.VerifyEmailAsync(emailVerification);
        }

    }
}
