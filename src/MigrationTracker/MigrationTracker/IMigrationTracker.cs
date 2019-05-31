using System.Threading;
using System.Threading.Tasks;

namespace MigrationTracker
{
    public interface IMigrationTracker
    {
        Task<bool> IsMigrationRequired(VersionInfo programVersion, CancellationToken cancel= default);
        Task MigrateTo(VersionInfo targetVersion, CancellationToken cancel = default);
    }
}