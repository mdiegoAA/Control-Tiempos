using Amezquita.ControlTiempos.Mobile.Infrastructure;
using Xamarin.Forms;

namespace Amezquita.ControlTiempos.Mobile.Features.Cargues
{
    public partial class RegistrarTiempoPage : ContentPage
    {
        public RegistrarTiempoPage()
        {
            InitializeComponent();

            BindingContext = new RegistrarTiempoViewModel(Navigation,
                                                          App.Container.GetInstance<ICarguesService>());
        }
    }
}