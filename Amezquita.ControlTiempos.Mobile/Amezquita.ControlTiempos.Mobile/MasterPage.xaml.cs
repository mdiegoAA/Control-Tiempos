using Amezquita.ControlTiempos.Mobile.Features.Accounts;
using Amezquita.ControlTiempos.Mobile.Features.Cargues;
using System;
using Amezquita.ControlTiempos.Mobile.Infrastructure;
using Xamarin.Forms;

namespace Amezquita.ControlTiempos.Mobile
{
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            if (App.AccessToken == null || App.AccessToken.ToString() == "Unknown identifier: App") await Navigation.PushAsync(new LoginPage());
        }

        public async void RegistrarTiempos(object sender, EventArgs e) => await Navigation.PushAsync(new RegistrarTiempoPage());

        public async void Historial(object sender, EventArgs e) => await Navigation.PushAsync(new HistorialPage());

        public async void CerrarSesion(object sender, EventArgs e)
        {
            var removido = App.RemoveToken;
            App.AccessToken = null;
            await Navigation.PushAsync(new LoginPage());
        }
    }
}