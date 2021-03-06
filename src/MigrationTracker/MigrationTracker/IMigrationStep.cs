﻿using System.Threading.Tasks;

namespace MigrationTracker
{
    /// <summary>
    /// Base interface for single migration step.
    /// </summary>
    public interface IMigrationStep
    {
        /// <summary>
        /// Version of data after migration was performed.
        /// </summary>
        /// <remarks>Have to be StartVersion + 1</remarks>
        long TargetVersion { get; }
        /// <summary>
        /// Version of data before migration gets performed.
        /// </summary>
        /// <remarks>Have to be TargetVersion - 1</remarks>
        long StartVersion { get; }
        /// <summary>
        ///Pre check to validate if all data is in correct state for migration.
        /// </summary>
        /// <returns></returns>
        Task<bool> CanMigrate();
        /// <summary>
        /// Performs an single migration step.
        /// </summary>
        Task Migrate();
    }
}