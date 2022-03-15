﻿using Marvelous.Contracts;

namespace MarvelousReportMicroService.API.Models.Request
{
    public class LeadSearchRequest
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime? StartBirthDate { get; set; }
        public DateTime? EndBirthDate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Role? Role { get; set; }
        public bool? IsBanned { get; set; }
    }
}