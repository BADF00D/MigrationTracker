using System;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationTracker
{
    public static class Factory
    {
        public static IMigrationTracker Create(
            IProvideDataVersionInfo versionInfoProvider,
            params IMigrationStep[] migrationSteps)
        {
            throw new NotImplementedException();
        }

        public static IProvideDataVersionInfo CreateDataVersionProvider(Func<CancellationToken,Task<VersionInfo>> read, Func<VersionInfo, CancellationToken, Task> update)
        {
            throw new NotImplementedException();
        }
    }
}