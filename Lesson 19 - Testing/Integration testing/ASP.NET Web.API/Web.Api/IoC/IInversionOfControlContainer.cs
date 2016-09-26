using System;
using System.Collections.Generic;

namespace Web.Api.IoC
{
    public interface IInversionOfControlContainer : IDisposable
    {
        object Resolve(Type serviceType);

        IEnumerable<object> ResolveAll(Type serviceType);

        IInversionOfControlContainer CreateScope();
    }
}