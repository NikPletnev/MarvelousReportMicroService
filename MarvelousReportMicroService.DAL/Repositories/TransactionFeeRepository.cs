﻿using Dapper;
using MarvelousReportMicroService.DAL.Configuration;
using MarvelousReportMicroService.DAL.Helpers;
using MarvelousReportMicroService.DAL.Models;
using Microsoft.Extensions.Options;
using System.Data;

namespace MarvelousReportMicroService.DAL.Repositories
{
    public class TransactionFeeRepository : BaseRepository, ITransactionFeeRepository
    {
        public TransactionFeeRepository(IOptions<DbConfiguration> options) : base(options)
        {

        }

        public async Task AddTransactionFee(TransactionFee feeModel)
        {
            using IDbConnection connection = ProvideConnection();

            await connection
                   .QueryAsync(
                   Queries.AddTransactionFee
                   , new
                   {
                       feeModel.IdTransaction,
                       feeModel.AmountComission
                   }
                   , commandType: CommandType.StoredProcedure);
        }
    }
}
