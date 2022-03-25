﻿using Marvelous.Contracts.Enums;

namespace MarvelousReportMicroService.BLL.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public Currency Currency { get; set; }
        public int Rate { get; set; }
    }
}