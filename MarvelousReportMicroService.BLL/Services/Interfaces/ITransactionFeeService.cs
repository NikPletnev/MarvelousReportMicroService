﻿using MarvelousReportMicroService.BLL.Models;

namespace MarvelousReportMicroService.BLL.Services
{
    public interface ITransactionFeeService
    {
        Task AddTransactionFee(TransactionFeeModel model);
    }
}