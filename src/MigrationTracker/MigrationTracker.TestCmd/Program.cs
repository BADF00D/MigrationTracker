using System;
using System.Threading.Tasks;

namespace MigrationTracker.TestCmd {
    class Program {
        public static readonly VersionInfo CurrentVersion = new VersionInfo(0, new DateTimeOffset(2019,05,31,13,51,00, TimeSpan.FromHours(1)));

        public static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }

        private static async Task Run()
        {
            var versionProvider = Factory.CreateDataVersionProvider(
                cancel => Task.FromResult(CurrentVersion),
                (c, vi) => Task.FromResult(0) //fake write call
            );
            var tracker = Factory.Create(versionProvider);

            if (await tracker.IsMigrationRequired(CurrentVersion))
            {
                await tracker.MigrateTo(CurrentVersion);
            }
        }
    }
}
