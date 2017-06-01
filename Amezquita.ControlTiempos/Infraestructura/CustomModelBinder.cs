using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public class PropertyModelBinderAttribute : Attribute
    {
        public PropertyModelBinderAttribute(Type binderType)
        {
            BinderType = binderType;
        }

        public Type BinderType { get; private set; }
    }

    public class UserNameModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
                return controllerContext.HttpContext.User.Identity.Name;

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "The user is not logged in.");

            return null;
        }
    }

    public class UserIdModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
                return Guid.Parse(controllerContext.HttpContext.User.Identity.GetUserId());

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "The user is not logged in.");

            return null;
        }
    }

    public class CustomModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            var contains = controllerContext.RouteData.Values.Keys.Any(key => key.ToLower() == propertyDescriptor.Name.ToLower());
            var propertyBinder = propertyDescriptor.Attributes.OfType<PropertyModelBinderAttribute>().FirstOrDefault();

            if (!contains && propertyBinder != null)
            {
                IModelBinder modelBinder = (IModelBinder)DependencyResolver.Current.GetService(propertyBinder.BinderType);
                var value = modelBinder.BindModel(controllerContext, bindingContext);
                propertyDescriptor.SetValue(bindingContext.Model, value);
            }
            else
            {
                base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
            }
        }
    }
}