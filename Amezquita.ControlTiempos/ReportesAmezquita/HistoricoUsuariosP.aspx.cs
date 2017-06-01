using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Amezquita.ControlTiempos.ReportesAmezquita
{
    public partial class HistoricoUsuariosP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            bool rSult = HttpContext.Current.User.Identity.IsAuthenticated;

            string name = HttpContext.Current.User.Identity.Name.ToString();
            //string nombreUsuarioRsuLT = reporteData.obtenerNombreUSuarioXid(name);
            UsuarioNombre.Text = name.ToString();
        }

        protected string ResolverUrl(object path)
        {
            var url = path.ToString();
            return ResolveClientUrl(url);
        }

    }
}