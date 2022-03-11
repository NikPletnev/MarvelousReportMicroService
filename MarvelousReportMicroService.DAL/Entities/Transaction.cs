﻿using Marvelous.Contracts;

namespace MarvelousReportMicroService.DAL.Entityes
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public Currency Currency { get; set; }
    }
}