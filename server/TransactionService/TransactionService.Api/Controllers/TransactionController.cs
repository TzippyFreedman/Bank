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
        private readonly IMessageSession _messageSession;

        public TransactionController(IMapper mapper, ITransactionService transactionService, IMessageSession messageSession)
        {
            _mapper = mapper;
            _transactionService = transactionService;
            _messageSession = messageSession;
        }

        [HttpPost]
        public async Task<ActionResult> Post(TransactionDTO transaction)
        {
            TransactionModel transactionModel = _mapper.Map<TransactionModel>(transaction);
            transactionModel.Status = TransactionStatus.Pending;
            TransactionModel newTransactionModel = await _transactionService.AddAsync(transactionModel);
            int amountToTransactionInCents = (int)Math.Round(transaction.Amount * 100);
            await _messageSession.Publish<ITransactionRequestAdded>(message =>
            {
                message.TransactionId = newTransactionModel.Id;
                message.Amount = amountToTransactionInCents;
                message.SrcAccountId = transaction.SrcAccountId;
                message.DestAccountId = transaction.DestAccountId;
            });
            return Ok();
        }
    }
}