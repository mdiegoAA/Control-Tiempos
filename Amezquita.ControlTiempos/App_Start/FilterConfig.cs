using Amezquita.ControlTiempos.Infraestructura;
using System.Web.Mvc;

namespace Amezquita.ControlTiempos
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleUIExceptionAttribute());
            filters.Add(new ValidatorActionFilter());
            filters.Add(new OutputCacheAttribute()
            {
                VaryByParam = "*",
                Duration = 0,
                NoStore = true
            });
        }
    }
}