using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TransferService.Api.DTO;
using TransferService.Services.Interfaces;
using TransferService.Services.Models;

namespace TransferService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITransferService _transferService;

        public TransferController(IMapper mapper, ITransferService transferService)
        {
            _mapper = mapper;
            _transferService = transferService;
        }


        [HttpPost]
        public async Task Post(TransferDTO measure)
        {
/*            TransferModel measureModel = _mapper.Map<TransferModel>(measure);
            measureModel.Status = TransferStatus.Pending;
            TransferModel newMeasureModel = await transferService.Add(measureModel);

            await _messageSession.Publish<IMeasureAdded>(message =>
            {
                message.MeasureId = newMeasureModel.Id;
                message.Weight = measure.Weight;
                message.UserFileId = measure.UserFileId;
            });*/


        }
    }
}