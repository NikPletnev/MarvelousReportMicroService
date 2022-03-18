﻿using MarvelousReportMicroService.BLL.Models;


namespace MarvelousReportMicroService.BLL.Services
{
    public interface ILeadService
    {
        List<LeadModel> GetAllLeads();
        List<LeadModel> GetLeadByParameters(LeadSearchModel model);
        List<LeadModel> GetLeadsByOffsetAndFetchParameters(LeadSerchWithOffsetAndFetchModel model);
    }
}