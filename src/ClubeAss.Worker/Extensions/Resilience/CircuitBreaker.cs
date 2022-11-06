using Polly;
using Polly.CircuitBreaker;
using System;

namespace ClubeAss.Worker.Resilience
{

    public static class CircuitBreaker
    {
        public static AsyncCircuitBreakerPolicy CreatePolicy()
        {
            return Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(20),
                    onBreak: (ex, @break) =>
                    {
                        Console.WriteLine("Open (onBreak)");
                    },
                    onReset: () =>
                    {
                        Console.WriteLine("Closed (onReset)");

                    },
                    onHalfOpen: () =>
                    {
                        Console.WriteLine("Meio Aberto (onHalfOpen)");
                    });
        }
    }
}
