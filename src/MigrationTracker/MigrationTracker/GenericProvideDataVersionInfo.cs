using System;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationTracker
{
    internal class GenericProvideDataVersionInfo : IProvideDataVersionInfo
    {
        private readonly Func<CancellationToken, Task<VersionInfo>> _read;
        private readonly Func<VersionInfo, CancellationToken, Task> _save;

        public GenericProvideDataVersionInfo(Func<CancellationToken,Task<VersionInfo>> read, Func<VersionInfo, CancellationToken, Task> save)
        {
            _read = read;
            _save = save;
        }

        public Task<VersionInfo> Read(CancellationToken cancel = default)
        {
            return _read(cancel);
        }

        public Task Save(VersionInfo versionInfo, CancellationToken cancel = default)
        {
            return _save(versionInfo, cancel);
        }
    }
}