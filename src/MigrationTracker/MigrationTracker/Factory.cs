using System;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationTracker
{
    public static class Factory
    {
        public static IMigrationTracker CreateMigrationTracker(
            IProvideDataVersionInfo versionInfoProvider,
            IMigrationStep[] migrationSteps)
        {
            if (versionInfoProvider == null) throw new ArgumentNullException(nameof(versionInfoProvider));
            if (migrationSteps == null) throw new ArgumentNullException(nameof(migrationSteps));
            return new MigrationTrackerImplementation(versionInfoProvider, migrationSteps);
        }

        public static IProvideDataVersionInfo CreateDataVersionProvider(Func<CancellationToken,Task<VersionInfo>> read, Func<VersionInfo, CancellationToken, Task> save)
        {
            return new GenericProvideDataVersionInfo(read, save);
        }
    }
}