// ----------------------------------------------------------------------------------------------
// <copyright file="SplashPage.xaml.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Mobile</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Amezquita.ControlTiempos.Mobile
{
    public partial class SplashPage : ContentPage
    {
        #region C'tors

        public SplashPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Instance Methods

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //WaitActivityIndicatorSplash.IsRunning = true;
            await Task.Delay(TimeSpan.FromSeconds(5));
            WaitActivityIndicatorSplash.IsRunning = false;
            // again, here you might want to use instead XLab's ViewFactory.Create<FirstPageViewModel>()
            App.Current.MainPage = new NavigationPage(new MasterPage());
        }

        #endregion
    }
}
