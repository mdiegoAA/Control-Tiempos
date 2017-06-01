using Amezquita.ControlTiempos.Mobile.Infrastructure;
using Xamarin.Forms;

namespace Amezquita.ControlTiempos.Mobile.Features.Accounts
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new LoginViewModel(Navigation,
                                                App.Container.GetInstance<IStorage>(),
                                                App.Container.GetInstance<IAccountService>()
                                                );
        }
    }
}