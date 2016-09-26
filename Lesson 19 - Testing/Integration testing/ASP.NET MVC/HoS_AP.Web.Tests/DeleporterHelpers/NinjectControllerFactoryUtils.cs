using System.Linq;

namespace HoS_AP.Web.Tests.DeleporterHelpers
{
    //public static class NinjectControllerFactoryUtils
    //{
    //     public static void TemporarilyReplaceBinding<TService>(TService implementation)
    //     {
    //         // get a handle on the Ninject Kernel (how you do this will depend on how you setup the Kernel)
    //         var kernel = DIFactory.Kernel;
    //         // Remove existing bindings and replace with new one
    //         var originalBindings = kernel.GetBindings(typeof(TService)).ToList();
    //         foreach (var originalBinding in originalBindings)
    //             kernel.RemoveBinding(originalBinding);
    //         var replacementBinding = kernel.Bind<TService>().ToConstant(implementation).Binding;

    //         // Clear up by doing the reverse
    //         TidyupUtils.AddTidyupTask(() =>
    //         {
    //             kernel.RemoveBinding(replacementBinding);
    //             foreach (var originalBinding in originalBindings)
    //                 kernel.AddBinding(originalBinding);
    //         });
    //     }
    //}
}