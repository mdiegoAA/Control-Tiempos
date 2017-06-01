using Amezquita.ControlTiempos.Mobile.Features.Accounts;
using Amezquita.ControlTiempos.Mobile.Features.Cargues;
using Amezquita.ControlTiempos.Mobile.Infrastructure;
using SimpleInjector;
using Xamarin.Forms;

namespace Amezquita.ControlTiempos.Mobile
{
    public partial class App : Application
    {
        public static Container Container { get; private set; }

        public App()
        {
            MainPage = new SplashPage();
        }

        protected override void OnStart()
        {
            Container = new Container();

            Container.Register(() => DependencyService.Get<ISQLiteLocator>());
            Container.Register<IStorage, SQLiteStorage>();
            Container.Register(()=> { return AccessToken; });
            Container.Register<IHttpService, HttpService>();
            Container.Register<IAccountService, AccountService>();
            Container.Register<ICarguesService, CarguesService>();
        }

        private static AccessToken _acessToken;
        public static AccessToken AccessToken
        {
            get
            {
                if (_acessToken == null)
                {
                    var storage = Container.GetInstance<IStorage>();
                    _acessToken = storage.GetFirst<AccessToken>();
                }

                return _acessToken;
            }
            set { _acessToken = value; }
        }

        private static AccessToken _removeToken;

        public static AccessToken RemoveToken
        {
            get
            {
                if (_removeToken == null)
                {
                    var storage = Container.GetInstance<IStorage>();
                    var token = storage.GetFirst<AccessToken>();
                    storage.Delete(token);
                }

                return null;
            }
        }
    }
}