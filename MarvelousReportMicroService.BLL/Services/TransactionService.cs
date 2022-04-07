using MarvelousReportMicroService.DAL.Repositories;
using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.BLL.Models;
using AutoMapper;

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

        public async Task<List<TransactionModel>> GetTransactionsBetweenDatesByLeadId(
            int id,
            DateTime startDate,
            DateTime finishDate)
        {
            List<Transaction> transactions = await _transactionRepository
                .GetTransactionsBetweenDatesByLeadId(id, startDate, finishDate);

            return _mapper.Map<List<TransactionModel>>(transactions);
        }

        public async Task<List<TransactionModel>> GetTransactionsByAccountId(int id)
        {
            List<Transaction> transactions = await _transactionRepository
                .GetTransactionsByAccountId(id);

            return _mapper.Map<List<TransactionModel>>(transactions);
        }

        public async Task<List<TransactionModel>> GetServicePayTransactionsByLeadIdBetweenDate(
            int id, DateTime startDate, DateTime endDate)
        {
            List<Transaction> transactions = await _transactionRepository
                .GetServicePayTransactionsByLeadIdBetweenDate(id, startDate, endDate);

            return _mapper.Map<List<TransactionModel>>(transactions);
        }

        public async Task<int> GetCountLeadTransactionWithoutWithdrawal(int leadId)
        {
            return await _transactionRepository.GetCountLeadTransactionWithoutWithdrawal(leadId);
        }


        public async Task AddTransaction(TransactionModel model)
        {
            await _transactionRepository.AddTransaction(_mapper.Map<Transaction>(model));
        }

        public async Task<List<TransactionModel>> GetLeadTransactionsForTheLastMonth(int leadId = 0)
        {
            List<Transaction> transactions = await _transactionRepository.GetLeadTransactionsForTheLastMonth(leadId);

            return _mapper.Map<List<TransactionModel>>(transactions);
        }
    }
}