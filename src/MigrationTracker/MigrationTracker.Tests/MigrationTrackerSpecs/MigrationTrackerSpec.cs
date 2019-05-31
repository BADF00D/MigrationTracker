using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using MigrationTracker.Exceptions;
using NUnit.Framework;

namespace MigrationTracker.Tests.MigrationTrackerSpecs
{
    internal abstract class MigrationTrackerSpec : AsyncSpec
    {
        protected readonly MigrationTrackerImplementation Sut;
        protected readonly IProvideDataVersionInfo ProvideDataVersionInfo;

        public MigrationTrackerSpec()
        {
            ProvideDataVersionInfo = A.Fake<IProvideDataVersionInfo>();
            Sut = new MigrationTrackerImplementation(
                ProvideDataVersionInfo,
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
    internal class If_Version_is_0_and_IsMigrationRequired__is_called_with_ProgramVersion_0 : MigrationTrackerSpec
    {
        private bool _isRequired;

        protected override IEnumerable<IMigrationStep> CreateFakeMigrationSteps()
        {
            return Enumerable.Empty<IMigrationStep>();
        }

        protected override Task EstablishContext()
        {
            A.CallTo(() => ProvideDataVersionInfo.Read(A<CancellationToken>._))
                .Returns(new VersionInfo(0, DateTimeOffset.Now));
            return base.EstablishContext();
        }

        protected override async Task BecauseOf()
        {
            _isRequired = await Sut.IsMigrationRequired(new VersionInfo(0, DateTimeOffset.Now));
        }

        [Test]
        public void Then_result_should_be_False()
        {
            _isRequired.Should().BeFalse();
        }
    }

    [TestFixture]
    internal class If_Version_is_1_and_IsMigrationRequired_is_called_with_ProgramVersion_3 : MigrationTrackerSpec
    {
        private bool _isRequired;

        protected override IEnumerable<IMigrationStep> CreateFakeMigrationSteps()
        {
            return Enumerable.Empty<IMigrationStep>();
        }

        protected override Task EstablishContext()
        {
            A.CallTo(() => ProvideDataVersionInfo.Read(A<CancellationToken>._))
                .Returns(new VersionInfo(0, DateTimeOffset.Now));
            return base.EstablishContext();
        }

        protected override async Task BecauseOf()
        {
            _isRequired = await Sut.IsMigrationRequired(new VersionInfo(3, DateTimeOffset.Now));
        }

        [Test]
        public void Then_result_should_be_True()
        {
            _isRequired.Should().BeTrue();
        }
    }

    [TestFixture]
    internal class If_Version_is_1_and_IsMigrationRequired_is_called_with_ProgramVersion_0 : MigrationTrackerSpec
    {
        private Exception _catchedException;

        protected override IEnumerable<IMigrationStep> CreateFakeMigrationSteps()
        {
            return Enumerable.Empty<IMigrationStep>();
        }

        protected override Task EstablishContext()
        {
            A.CallTo(() => ProvideDataVersionInfo.Read(A<CancellationToken>._))
                .Returns(new VersionInfo(1, DateTimeOffset.Now));
            return base.EstablishContext();
        }

        protected override async Task BecauseOf()
        {
            try//todo there should be a better way the catching and storing the exception.
            {
                await Sut.IsMigrationRequired(new VersionInfo(0, DateTimeOffset.Now));
            }
            catch (Exception e)
            {
                _catchedException = e;
            }
        }

        [Test]
        public void Then_result_should_be_True()
        {
            _catchedException.Should().BeAssignableTo<MigrationException>();
        }
    }

    //[TestFixture]
    //internal class If_can_Migrate_is_called_but_there_are_no_migration_steps_to_perform : MigrationTrackerSpec
    //{
    //    protected override IEnumerable<IMigrationStep> CreateFakeMigrationSteps()
    //    {
    //        return Enumerable.Empty<IMigrationStep>();
    //    }
    //}
}