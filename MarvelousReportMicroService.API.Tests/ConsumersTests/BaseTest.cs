﻿using AutoMapper;
using MarvelousReportMicroService.API.Consumers;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace MarvelousReportMicroService.API.Tests.ConsumersTests
{
    public class BaseTest
    {
        protected IMapper _mapper;
        protected Mock<ILogger<AccountConsumer>> _logger;

        protected void VerifyLogger(LogLevel logLevel, String message)
        {
            _logger.Verify(
               x => x.Log(
                   logLevel,
                   It.IsAny<EventId>(),
                   It.Is<It.IsAnyType>((o, t) =>
                   string.Equals(message, o.ToString(),
                   StringComparison.InvariantCultureIgnoreCase)),
                   It.IsAny<Exception>(),
                   It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }
    }
}