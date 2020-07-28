using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Api.DTO;
using UserService.Services;
using UserService.Services.Interfaces;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsHistoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOperationsHistoryService _operationsHistoryService;

        public OperationsHistoryController(IMapper mapper, IOperationsHistoryService operationsHistoryService)
        {
            _mapper = mapper;
            _operationsHistoryService = operationsHistoryService;
        }

        [HttpGet]
        public Task<PaginationResult<HistoryOperationDTO>> Get()
        {


            PaginationResult<HistoryOperationDTO> result= _operationsHistoryService.Get();


        }
    }
}
