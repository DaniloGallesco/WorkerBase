using ClubeAss.Domain.Interface.Application;
using ClubeAss.Domain.Worker;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
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
        private readonly AsyncCircuitBreakerPolicy _circuitBreaker;

        public DefaultScopedProcessingService(ILogger<DefaultScopedProcessingService> logger, ICustomerApplication customerApplication, AsyncCircuitBreakerPolicy circuitBreaker)
        {
            _logger = logger;
            _customerApplication = customerApplication;
            _circuitBreaker = circuitBreaker;
        }
        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (LogContext.PushProperty("correlationId", Guid.NewGuid().ToString()))
                    {
                        _logger.LogInformation($"INICIO *{DateTime.Now:HH:mm:ss}*");

                        _logger.LogInformation($"circuitBreaker = {_circuitBreaker.CircuitState}");

                        if (_circuitBreaker.CircuitState != CircuitState.Open)
                        {
                            var resultado = await _circuitBreaker.ExecuteAsync(async () =>
                            {
                                var result = await _customerApplication.Add(new API.Customer.ViewModel.Customer.CustomerAddRequest() { Nome = Guid.NewGuid().ToString() });

                                return result.StatusCode == System.Net.HttpStatusCode.OK;
                            });
                        }
                        _logger.LogInformation($"FIM *{DateTime.Now:HH:mm:ss}*");
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"FIM *{DateTime.Now:HH:mm:ss}*");
                }
            }
        }
    }
}
