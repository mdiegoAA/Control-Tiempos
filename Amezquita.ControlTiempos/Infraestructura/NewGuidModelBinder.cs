using System;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public class NewGuidModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return Guid.NewGuid();
        }
    }
}