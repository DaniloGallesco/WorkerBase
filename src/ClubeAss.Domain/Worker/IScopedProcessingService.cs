using System.Threading;
using System.Threading.Tasks;

namespace ClubeAss.Domain.Worker
{
    public interface IScopedProcessingService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
