using MarvelousReportMicroService.DAL.Repositories;
using MarvelousReportMicroService.DAL.Entities;
using MarvelousReportMicroService.DAL.Models;
using MarvelousReportMicroService.BLL.Models;
using AutoMapper;
using MarvelousReportMicroService.BLL.Helpers;
using MarvelousReportMicroService.BLL.Exceptions;

namespace MarvelousReportMicroService.BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRequest _authRequest;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, IAuthRequest authRequest)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _authRequest = authRequest;
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

        public async Task<int> GetCountLeadTransactionWithoutWithdrawal(int leadId, string token)
        {
            if (!await _authRequest.GetRestResponse(token))
                throw new ForbiddenException("invalid token");

            return await _transactionRepository.GetCountLeadTransactionWithoutWithdrawal(leadId);
        }


        public async Task AddTransaction(TransactionModel model)
        {
            await _transactionRepository.AddTransaction(_mapper.Map<Transaction>(model));
        }

        public async Task<List<ShortTransactionModel>> GetLeadTransactionsForTheLastMonth(string token, int leadId = 0)
        {
            if (!await _authRequest.GetRestResponse(token))
                throw new ForbiddenException("invalid token");

            List<ShortTransaction> transactions = await _transactionRepository.GetLeadTransactionsForTheLastMonth(leadId);

            return _mapper.Map<List<ShortTransactionModel>>(transactions);
        }
    }
}