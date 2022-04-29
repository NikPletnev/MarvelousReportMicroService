﻿using AutoMapper;
using MarvelousReportMicroService.BLL.Models;
using MarvelousReportMicroService.DAL.Models;
using MarvelousReportMicroService.DAL.Repositories;

namespace MarvelousReportMicroService.BLL.Services
{
    public class TransactionFeeService : ITransactionFeeService
    {
        private readonly ITransactionFeeRepository _transactionFeeRepository;
        private readonly IMapper _mapper;

        public TransactionFeeService(ITransactionFeeRepository transactionFeeRepository, IMapper mapper)
        {
            _transactionFeeRepository = transactionFeeRepository;
            _mapper = mapper;
        }

        public async Task AddTransactionFee(TransactionFeeModel model)
        {
            await _transactionFeeRepository.AddTransactionFee(_mapper.Map<TransactionFee>(model));
        }

        public async Task<List<ProfitModel>> GetProfit(DateTime date)
        {
            var listProfit = await _transactionFeeRepository.GetProfit(date);
            var result = _mapper.Map<List<ProfitModel>>(listProfit);
            return result;
        }
    }
}
