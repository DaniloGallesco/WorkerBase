using ClubeAss.Domain.Interface.Application;
using ClubeAss.Domain.Worker;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClubeAss.Worker
{
    public class DefaultScopedProcessingService : IScopedProcessingService
    {
        private int _executionCount;
        private readonly ILogger<DefaultScopedProcessingService> _logger;
        private readonly ICustomerApplication _customerApplication;

        public DefaultScopedProcessingService(ILogger<DefaultScopedProcessingService> logger, ICustomerApplication customerApplication)
        {
            _logger = logger;
            _customerApplication = customerApplication;
        }
        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ++_executionCount;

                _logger.LogInformation(
                    "{ServiceName} working, execution count: {Count}",
                    nameof(DefaultScopedProcessingService),
                    _executionCount);

                using (LogContext.PushProperty("correlationId", Guid.NewGuid().ToString()))
                {
                    _logger.LogInformation("Log de fora");
                    _ = _customerApplication.Add(new API.Customer.ViewModel.Customer.CustomerAddRequest() { Nome = Guid.NewGuid().ToString() }).Result;
                    await Task.Delay(1000, stoppingToken);
                }

                await Task.Delay(10_000, stoppingToken);
            }
        }
    }
}
