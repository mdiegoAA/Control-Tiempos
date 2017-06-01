using Xamarin.Forms;

namespace Amezquita.ControlTiempos.Mobile.Features.Cargues
{
    public partial class HistorialPage : ContentPage
    {
        public HistorialPage()
        {
            InitializeComponent();

            BindingContext = new HistorialViewModel(App.Container.GetInstance<ICarguesService>());
        }
    }
}