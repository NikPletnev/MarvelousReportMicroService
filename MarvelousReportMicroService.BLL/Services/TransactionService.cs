using MarvelousReportMicroService.DAL.Repositories;
using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.DAL.Models;
using MarvelousReportMicroService.BLL.Models;
using AutoMapper;
using MarvelousReportMicroService.BLL.Helpers;

namespace MarvelousReportMicroService.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IRequestHelper _requestHelper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, IRequestHelper authRequest)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _requestHelper = authRequest;
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

        public async Task<List<ShortTransactionModel>> GetLeadTransactionsForTheLastMonth(int leadId = 0)
        {
            List<ShortTransaction> transactions = await _transactionRepository.GetLeadTransactionsForTheLastMonth(leadId);

            return _mapper.Map<List<ShortTransactionModel>>(transactions);
        }
    }
}