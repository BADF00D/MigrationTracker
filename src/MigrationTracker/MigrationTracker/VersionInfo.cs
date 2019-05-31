using System;

namespace MigrationTracker
{
    public sealed class VersionInfo
    {
        /// <summary>
        /// Version of data
        /// </summary>
        public long Version { get; }
        /// <summary>
        /// DateTimeOffset when Version was created.
        /// </summary>
        /// <remarks>Is informational purpose only.</remarks>
        public DateTimeOffset CreatedAt { get; }

        public VersionInfo(long version, DateTimeOffset createdAt)
        {
            Version = version;
            CreatedAt = createdAt;
        }
    }
}