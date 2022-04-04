﻿using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.BLL.Models;
using Marvelous.Contracts.ExchangeModels;
using Marvelous.Contracts.RequestModels;
using MassTransit;
using AutoMapper;


namespace MarvelousReportMicroService.API.Consumers
{
    public class LeadConsumer : IConsumer<LeadFullExchangeModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<LeadConsumer> _logger;
        private readonly ILeadService _leadService;

        public LeadConsumer(IMapper mapper, ILogger<LeadConsumer> logger, ILeadService leadService)
        {
            _mapper = mapper;
            _logger = logger;
            _leadService = leadService;
        }

        public async Task Consume(ConsumeContext<LeadFullExchangeModel> context) 
        {
            _logger.LogInformation($"Getting lead {context.Message.Id}");
            var model = _mapper.Map<LeadModel>(context.Message);
            _logger.LogInformation($"");
            var leadModel = _mapper.Map<LeadModel>(context.Message);
            if (await _leadService.GetLeadIdIfExist(leadModel.Id) == null)
            {
                await _leadService.AddLead(leadModel);
            }
            else
            {
                await _leadService.UpdateLead(leadModel);
            }
            
        }
    }
}