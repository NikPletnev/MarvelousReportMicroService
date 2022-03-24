﻿using Marvelous.Contracts.Enums;

namespace MarvelousReportMicroService.DAL.Entities
{
    public class Lead
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int? BirthDay { get; set; }
        public int? BirthMonth { get; set; }
        public int? BirthYear { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public List<Account>? Accounts { get; set; }
        public Role? Role { get; set; }
        public bool? IsBanned { get; set; }
    }
}