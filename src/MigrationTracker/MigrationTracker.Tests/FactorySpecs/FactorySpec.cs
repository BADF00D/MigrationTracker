using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace MigrationTracker.Tests.FactorySpecs
{
    internal class FactoryCreateDataVersionProviderSpec : Spec
    {
        protected VersionInfo AfterMigrationVersionInfo;
        protected readonly IProvideDataVersionInfo VersionInfoProvider;
        protected readonly VersionInfo BeforeMigrationVersionInfo = new VersionInfo(13, DateTimeOffset.Now);

        public FactoryCreateDataVersionProviderSpec()
        {
            VersionInfoProvider = Factory.CreateDataVersionProvider(
                _ => Task.FromResult(BeforeMigrationVersionInfo),
                (vi, _) =>
                {
                    AfterMigrationVersionInfo = vi;
                    return Task.CompletedTask;
                });
        }
    }

    [TestFixture]
    internal class If_Read_is_called : FactoryCreateDataVersionProviderSpec
    {
        protected internal VersionInfo VersionReadByProvider;

        protected override void BecauseOf()
        {
            VersionReadByProvider = VersionInfoProvider.Read().Result;
        }

        [Test]
        public void Then_Provide_should_return_the_version_before_upgrade()
        {
            VersionReadByProvider.Should().Be(BeforeMigrationVersionInfo);
        }
    }

    [TestFixture]
    internal class If_Save_is_called : FactoryCreateDataVersionProviderSpec
    {
        protected internal VersionInfo NewVersionInfo = new VersionInfo(14, DateTimeOffset.Now);

        protected override void BecauseOf()
        {
            VersionInfoProvider.Save(NewVersionInfo)
                .GetAwaiter().GetResult();
        }

        [Test]
        public void Then()
        {
            AfterMigrationVersionInfo.Should().Be(NewVersionInfo);
        }
    }
}