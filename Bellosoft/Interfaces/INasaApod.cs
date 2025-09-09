using Bellosoft.Models;

namespace Bellosoft.Interfaces
{
    public interface INasaApod
    {
        Task<NasaApod?> GetTodayAsync(CancellationToken ct = default);
    }
}
