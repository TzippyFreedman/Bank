using AutoMapper;
using Messages.Events;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using System;
using System.Threading.Tasks;
using TransactionService.Api.DTO;
using TransactionService.Contract.Enums;
using TransactionService.Contract.Models;
using TransactionService.Services.Interfaces;

namespace TransactionService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;


        public TransactionController(IMapper mapper, ITransactionService transactionService)
        {
            _mapper = mapper;
            _transactionService = transactionService;
        }

      
       [HttpPost]
        public async Task<ActionResult> Post(TransactionDTO transaction)
        {
            TransactionModel transactionModel = _mapper.Map<TransactionModel>(transaction);
            transactionModel.Status = TransactionStatus.Pending;
            await _transactionService.AddAsync(transactionModel);
            return Ok();
        }

        [HttpGet]
        [Route("{transactionId}")]
        public async Task<ActionResult<TransactionDTO>> GetAsync(Guid transactionId)
        {
            TransactionModel transactionModel = await _transactionService.GetByIdAsync(transactionId);
            TransactionDTO transaction = _mapper.Map<TransactionDTO>(transactionModel);
            return transaction;
        }
    }
}