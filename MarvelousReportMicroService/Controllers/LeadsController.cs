﻿using MarvelousReportMicroService.API.Models.Request;
using MarvelousReportMicroService.BLL.Services;
using MarvelousReportMicroService.API.Models;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Enums;
using Marvelous.Contracts.ExchangeModels;
using Marvelous.Contracts.Enums;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Marvelous.Contracts.Urls;

namespace MarvelousReportMicroService.API.Controllers
{
    [ApiController]
    [Route(ReportingUrls.ApiLeads)]
    public class LeadsController : Controller
    {
        private readonly ILeadService _leadService;
        private readonly IMapper _mapper;
        private readonly ILogger<LeadsController> _logger;

        public LeadsController(IMapper mapper, ILeadService leadService, ILogger<LeadsController> logger)
        {
            _leadService = leadService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("search")]
        public ActionResult<List<LeadResponse>> GetLeadByParameters(
            [FromQuery] int? id,
            [FromQuery] string? name,
            [FromQuery] LeadSearchParams? nameParam,
            [FromQuery] string? lastName,
            [FromQuery] LeadSearchParams? lastNameParam,
            [FromQuery] DateTime? startBirthDate,
            [FromQuery] DateTime? endBirthDate,
            [FromQuery] string? email,
            [FromQuery] LeadSearchParams? emailParam,
            [FromQuery] string? phone,
            [FromQuery] LeadSearchParams? phoneParam,
            [FromQuery] Role? role,
            [FromQuery] bool? isBanned)
        {
            _logger.LogInformation($"Request to get all leads for certain parameters");

            LeadSearchRequest leadModel = new LeadSearchRequest()
            {
                Id = id,
                Name = name,
                NameParam = nameParam,
                LastName = lastName,
                LastNameParam = lastNameParam,
                StartBirthDate = startBirthDate,
                EndBirthDate = endBirthDate,
                Email = email,
                EmailParam = emailParam,
                PhoneParam = phoneParam,
                Phone = phone,
                Role = role,
                IsBanned = isBanned
            };

            List<LeadModel> leads = _leadService.GetLeadByParameters(_mapper.Map<LeadSearchModel>(leadModel));

            _logger.LogInformation($"Response to a request to get all leads for certain parameters");
            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }

        [HttpGet("take-leads-in-range")]
        public async Task<ActionResult<List<LeadResponse>>> GetLeadWithOffsetAndFetch([FromQuery] int offset, [FromQuery] int fetch)
        {
            LeadSerchWithOffsetAndFetchRequest leadModel = new LeadSerchWithOffsetAndFetchRequest()
            {
                Offset = offset,
                Fetch = fetch
            };

            _logger.LogInformation($"Request to get for {offset} leads starting with {fetch}");

            var leads = await _leadService.GetLeadsByOffsetAndFetchParameters(_mapper.Map<LeadSerchWithOffsetAndFetchModel>(leadModel));

            _logger.LogInformation($"Response to a request to get for {offset} leads starting with {fetch}");
            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }

        [HttpGet("service-subscribers")]
        public async Task<ActionResult<List<LeadResponse>>> GetLeadsByServiceId([FromQuery] int serviceId)
        {
            _logger.LogInformation($"Request to get all service({serviceId}) subscribers");
            var leads = await _leadService.GetLeadsByServiceId(serviceId);

            _logger.LogInformation($"Response to a request to get all service({serviceId}) subscribers");
            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }

        [HttpGet("bithday-by-date")]
        public async Task<ActionResult<List<LeadResponse>>> GetBirthdayLead(
            [FromQuery]int day = 0,
            [FromQuery]int month = 0)
        {
            _logger.LogInformation($"Request to get all birthdy {month}\\{day} leads");
            var leads = await _leadService.GetBirthdayLead(day, month);

            _logger.LogInformation($"Response to a request to get all get all birthdy {month}\\{day} leads");
            return Ok(_mapper.Map<List<LeadResponse>>(leads));
        }

        [HttpGet("count-leads")]
        public async Task<ActionResult> GetLeadsCountByRole([FromQuery] Role role)
        {
            _logger.LogInformation($"Request to get count of leads by role");
            var leads = await _leadService.GetLeadsCountByRole(role);
            _logger.LogInformation("Response to get count of leads by role");
            return Ok(leads);
        }

        [HttpGet(ReportingUrls.ApiLeads)]
        [ProducesResponseType(typeof(LeadAuthExchangeModel), 200)]
        public async Task<ActionResult<List<LeadAuthExchangeModel>>> GetAllLeads()
        {
            _logger.LogInformation($"Request to get all leads for auth");
            var leads = await _leadService.GetAllLeads();

            _logger.LogInformation($"Response to get all leads for auth");
            return Ok(_mapper.Map<List<LeadAuthExchangeModel>>(leads));
        }
    }
}