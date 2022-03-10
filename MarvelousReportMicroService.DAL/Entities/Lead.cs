﻿using MarvelousReportMicroService.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousReportMicroService.DAL.Entityes
{
    public class Lead
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public List<Account> Accounts { get; set; }
        public Role Role { get; set; }
        public bool IsBanned { get; set; }
    }
}
