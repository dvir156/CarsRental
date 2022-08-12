using Autofac.Core;
using Autofac.Extras.FakeItEasy;
using System;

namespace CarsCatalog.Tests.Infrastructure
{
    internal class TestsInjector : IDisposable
    {
        private readonly AutoFake _autoFake;
        public TestsInjector()
        {
            _autoFake = new AutoFake();
        }

        public T Resolve<T>(params Parameter[] parameters)
        {
            return _autoFake.Resolve<T>(parameters);
        }

        public TService Provide<TService>(TService instance) where TService : class
        {
            return _autoFake.Provide(instance);
        }

        public void Dispose()
        {
            _autoFake.Dispose();
        }
    }
}
