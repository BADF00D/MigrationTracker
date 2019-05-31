using System.Threading;
using System.Threading.Tasks;

namespace MigrationTracker
{
    /// <summary>
    /// Provider to retrieve and update current version of the data.
    /// </summary>
    public interface IProvideDataVersionInfo
    {
        Task<VersionInfo> Read(CancellationToken cancel = default);
        Task Save(VersionInfo versionInfo, CancellationToken cancel = default);
    }
}