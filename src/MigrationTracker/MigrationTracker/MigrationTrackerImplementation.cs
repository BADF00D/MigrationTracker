using System;
using System.Threading;
using System.Threading.Tasks;
using MigrationTracker.Exceptions;

namespace MigrationTracker
{
    internal class MigrationTrackerImplementation : IMigrationTracker
    {
        private readonly IMigrationStep[] _migrationSteps;
        private readonly IProvideDataVersionInfo _versionInfoProvider;

        public MigrationTrackerImplementation(IProvideDataVersionInfo versionInfoProvider, IMigrationStep[] migrationSteps)
        {
            _versionInfoProvider = versionInfoProvider;
            _migrationSteps = migrationSteps;
        }

        public async Task<bool> IsMigrationRequired(VersionInfo programVersion, CancellationToken cancel = default)
        {
            var currentVersion = await _versionInfoProvider.Read(cancel);
            if(currentVersion > programVersion) throw new MigrationException("Your data is newer then your program.");

            return currentVersion < programVersion;
        }

        public Task<bool> CanMigrateTo(VersionInfo versionInfo, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task MigrateTo(VersionInfo targetVersion, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}