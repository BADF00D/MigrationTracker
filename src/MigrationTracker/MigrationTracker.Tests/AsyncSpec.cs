using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MigrationTracker.Tests
{
    [DebuggerStepThrough, DebuggerNonUserCode]
    public class AsyncSpec
    {
        private readonly List<Action> _disposeActions = new List<Action>();

        public AsyncSpec()
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }

        [DebuggerStepThrough]
        [OneTimeSetUp]
        public Task TestFixtureSetUp()
        {
            EstablishContext();
            return BecauseOf();
        }

        [DebuggerStepThrough]
        [OneTimeTearDown]
        public Task TearDown()
        {
            foreach (var disposeAction in _disposeActions)
            {
                disposeAction();
            }
            return Cleanup();
        }

        /// <summary>
        ///     Test setup. Place your initialization code here.
        /// </summary>
        [DebuggerStepThrough]
        protected virtual Task EstablishContext()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Test run. Place the tested method / action here.
        /// </summary>
        [DebuggerStepThrough]
        protected virtual Task BecauseOf()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Test clean. Close/delete files, close database connections ..
        /// </summary>
        [DebuggerStepThrough]
        protected virtual Task Cleanup()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Creates an Action delegate.
        /// </summary>
        /// <param name="func">Method the shall be created as delegate.</param>
        /// <returns>A delegate of type <see cref="Action" /></returns>
        protected Action Invoking(Action func)
        {
            return func;
        }

        protected void DisposeOnTearDown(IDisposable disposable)
        {
            _disposeActions.Add(() => disposable?.Dispose());
        }
    }
}