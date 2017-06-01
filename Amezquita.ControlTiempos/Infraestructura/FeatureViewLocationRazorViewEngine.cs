using System.Web.Mvc;

namespace Amezquita.ControlTiempos.Infraestructura
{
    public class FeatureViewLocationRazorViewEngine : RazorViewEngine
    {
        public FeatureViewLocationRazorViewEngine()
        {
            ViewLocationFormats =
            MasterLocationFormats = 
            PartialViewLocationFormats = new[]
            {
                "~/Features/{1}/{0}.cshtml",
                "~/Features/{1}/{0}.vbhtml",
                "~/Features/Shared/{0}.cshtml",
                "~/Features/Shared/{0}.vbhtml",
            };

            AreaViewLocationFormats =
            AreaMasterLocationFormats =
            AreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Features/{1}/{0}.cshtml",
                "~/Areas/{2}/Features/{1}/{0}.vbhtml",
                "~/Areas/{2}/Features/Shared/{0}.cshtml",
                "~/Areas/{2}/Features/Shared/{0}.vbhtml",
            };
        }
    }
}