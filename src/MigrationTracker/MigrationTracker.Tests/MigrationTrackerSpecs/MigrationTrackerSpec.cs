using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace MigrationTracker.Tests.MigrationTrackerSpecs
{
    internal abstract class MigrationTrackerSpec : AsyncSpec
    {
        protected readonly MigrationTrackerImplementation _sut;
        protected IProvideDataVersionInfo _provideDataVersionInfo;

        public MigrationTrackerSpec()
        {
            _provideDataVersionInfo = A.Fake<IProvideDataVersionInfo>();
            _sut = new MigrationTrackerImplementation(
                _provideDataVersionInfo,
                CreateFakeMigrationSteps().ToArray());
        }

        protected abstract IEnumerable<IMigrationStep> CreateFakeMigrationSteps();

        protected IMigrationStep CreateMigration(int fromVersion, int toVersion)
        {
            var step = A.Fake<IMigrationStep>(o => o.Strict());
            A.CallTo(() => step.StartVersion).Returns(fromVersion);
            A.CallTo(() => step.TargetVersion).Returns(toVersion);
            return step;
        }
    }

    [TestFixture]
    internal class If_Version_is_0_and_IsMigrationRequired__is_called_with_TargetVersion_0 : MigrationTrackerSpec
    {
        private bool _isRequired;

        protected override IEnumerable<IMigrationStep> CreateFakeMigrationSteps()
        {
            return Enumerable.Empty<IMigrationStep>();
        }

        protected override Task EstablishContext()
        {
            A.CallTo(() => _provideDataVersionInfo.Read(A<CancellationToken>._))
                .Returns(new VersionInfo(0, DateTimeOffset.Now));
            return base.EstablishContext();
        }

        protected override async Task BecauseOf()
        {
            _isRequired = await _sut.IsMigrationRequired(new VersionInfo(0, DateTimeOffset.Now));
        }

        [Test]
        public void Then_result_should_be_False()
        {
            _isRequired.Should().BeFalse();
        }
    }

    [TestFixture]
    internal class If_Version_is_1_and_IsMigrationRequired_is_called_with_TargetVersion_3 : MigrationTrackerSpec
    {
        private bool _isRequired;

        protected override IEnumerable<IMigrationStep> CreateFakeMigrationSteps()
        {
            return Enumerable.Empty<IMigrationStep>();
        }

        protected override Task EstablishContext()
        {
            A.CallTo(() => _provideDataVersionInfo.Read(A<CancellationToken>._))
                .Returns(new VersionInfo(0, DateTimeOffset.Now));
            return base.EstablishContext();
        }

        protected override async Task BecauseOf()
        {
            _isRequired = await _sut.IsMigrationRequired(new VersionInfo(3, DateTimeOffset.Now));
        }

        [Test]
        public void Then_result_should_be_True()
        {
            _isRequired.Should().BeTrue();
        }
    }
}