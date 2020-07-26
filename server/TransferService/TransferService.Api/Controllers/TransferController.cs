using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messages.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NServiceBus;
using TransferService.Api.DTO;
using TransferService.Contract.Enums;
using TransferService.Contract.Models;
using TransferService.Services.Interfaces;

namespace TransferService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransferService _transferService;
        private readonly IMessageSession _messageSession;

        public TransferController(IMapper mapper, ITransferService transferService, IMessageSession messageSession)
        {
            _mapper = mapper;
            _transferService = transferService;
            _messageSession = messageSession;
        }


        [HttpPost]
        public async Task<ActionResult> Post(TransferDTO transfer)
        {
            TransferModel transferModel = _mapper.Map<TransferModel>(transfer);
            transferModel.Status = TransferStatus.Pending;
            TransferModel newTransferModel = await _transferService.Add(transferModel);

            int amountToTransferInCents = (int)Math.Round(transfer.Amount * 100);
            await _messageSession.Publish<ITransferRequestAdded>(message =>
            {
                message.TransferId = newTransferModel.Id;
                message.Amount = amountToTransferInCents;
                message.SrcAccountId = transfer.SrcAccount;
                message.DestAccountId = transfer.DestAccount;
            });

            return Ok();
        }
    }
}