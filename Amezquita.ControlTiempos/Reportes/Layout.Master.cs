using System.Web.UI;

namespace Amezquita.ControlTiempos.Reportes
{
    public partial class Layout : MasterPage
    {
        protected string ResolverUrl(object path)
        {
            var url = path.ToString();
            return ResolveClientUrl(url);
        }
    }
}