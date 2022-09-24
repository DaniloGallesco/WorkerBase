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
                using (LogContext.PushProperty("correlationId", Guid.NewGuid().ToString()))
                {
                    try
                    {
                        _logger.LogInformation("Inicio");
                        //_ = _customerApplication.Add(new API.Customer.ViewModel.Customer.CustomerAddRequest() { Nome = Guid.NewGuid().ToString() }).Result;
                        _logger.LogInformation("FIM");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro");
                    }
                }
            }
        }
    }
}
