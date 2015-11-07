using System.ComponentModel;
using System.Web.Mvc;

namespace ValidationAttributes
{
	public class MyModelBinder : DefaultModelBinder
	{
		protected override bool OnModelUpdating(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return base.OnModelUpdating(controllerContext, bindingContext);
		}

		protected override bool OnPropertyValidating(ControllerContext controllerContext, ModelBindingContext bindingContext,
			PropertyDescriptor propertyDescriptor, object value)
		{
			return base.OnPropertyValidating(controllerContext, bindingContext, propertyDescriptor, value);
		}

		protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
	PropertyDescriptor propertyDescriptor, object value)
		{
			base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
		}

		protected override void OnPropertyValidated(ControllerContext controllerContext, ModelBindingContext bindingContext,
			PropertyDescriptor propertyDescriptor, object value)
		{
			base.OnPropertyValidated(controllerContext, bindingContext, propertyDescriptor, value);
		}

		protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			base.OnModelUpdated(controllerContext, bindingContext);
		}
	}
}