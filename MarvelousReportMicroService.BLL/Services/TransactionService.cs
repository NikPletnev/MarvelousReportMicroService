﻿using AutoMapper;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Entityes;
using MarvelousReportMicroService.DAL.Repositories;

namespace MarvelousReportMicroService.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public List<TransactionModel> GetTransactionsBetweenDatesByLeadId(
            int id
            , DateTime startDate
            , DateTime finishDate)
        {
            List<Transaction> transactions = _transactionRepository
                .GetTransactionsBetweenDatesByLeadId(id, startDate, finishDate);

            return _mapper.Map<List<TransactionModel>>(transactions);
        }
    }
}