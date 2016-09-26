using System;
using System.Collections.Generic;

namespace HoS_AP.Misc
{
    public interface IInversionOfControlContainer : IDisposable
    {
        object Resolve(Type serviceType);

        IEnumerable<object> ResolveAll(Type serviceType);

        bool IsRegistered(Type serviceType);

        IInversionOfControlContainer CreateScope();
    }
}