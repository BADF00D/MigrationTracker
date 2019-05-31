using System;
using System.Data.Common;

namespace MigrationTracker
{
    public sealed class VersionInfo : IComparable<VersionInfo>, IEquatable<VersionInfo>
    {
        /// <summary>
        /// Version of data
        /// </summary>
        public long Version { get; }
        /// <summary>
        /// DateTimeOffset when Version was created.
        /// </summary>
        /// <remarks>For informational purpose only.</remarks>
        public DateTimeOffset CreatedAt { get; }

        public VersionInfo(long version, DateTimeOffset createdAt)
        {
            Version = version;
            CreatedAt = createdAt;
        }

        public static bool operator <(VersionInfo left, VersionInfo right)
        {
            return left.Version < right.Version;
        }
        public static bool operator > (VersionInfo left, VersionInfo right)
        {
            return right.Version < left.Version;
        }

        public int CompareTo(VersionInfo other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Version.CompareTo(other.Version);
        }

        public bool Equals(VersionInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Version == other.Version && CreatedAt.Equals(other.CreatedAt);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is VersionInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Version.GetHashCode() * 397) ^ CreatedAt.GetHashCode();
            }
        }

        public static bool operator ==(VersionInfo left, VersionInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VersionInfo left, VersionInfo right)
        {
            return !Equals(left, right);
        }
    }
}