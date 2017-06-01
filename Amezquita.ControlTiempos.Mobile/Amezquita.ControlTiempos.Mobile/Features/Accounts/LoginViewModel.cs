using Amezquita.ControlTiempos.Mobile.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace Amezquita.ControlTiempos.Mobile.Features.Accounts
{
    public class LoginViewModel : ObservableBase
    {
        private readonly INavigation _navigation;
        private readonly IAccountService _service;
        private readonly IStorage _storage;

        public LoginViewModel(INavigation navigation,  IStorage storage, IAccountService service)
        {
            if (navigation == null)
                throw new ArgumentNullException(nameof(navigation));

            _navigation = navigation;

            if (service == null)
                throw new ArgumentNullException(nameof(service));

            _service = service;

            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;
            
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isAuthorized = false;
        public bool IsAuthorized
        {
            get { return _isAuthorized; }
            set { SetProperty(ref _isAuthorized, value); }
        }

        private string _usuario = string.Empty;
        public string Usuario
        {
            get { return _usuario; }
            set
            {
                if (SetProperty(ref _usuario, value, nameof(Usuario)))
                    LoginCommand.ChangeCanExecute();
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set
            {
                if (SetProperty(ref _password, value, nameof(Password)))
                    LoginCommand.ChangeCanExecute();
            }
        }

        private Command _loginCommand;
        public Command LoginCommand => _loginCommand ?? (_loginCommand = new Command(async () =>
        {
            IsBusy = true;
            IsAuthorized = false;

            var accessToken = await _service.LoginAsync(new LoginDTO
            {
                Usuario = Usuario,
                Password = Password
            });

            if (!string.IsNullOrEmpty(accessToken.Token))
            {
                IsAuthorized = true;
                _storage.Save(new AccessToken
                              {
                                  ExpiresIn = accessToken.ExpiresIn,
                                  Token = accessToken.Token,
                                  Type = accessToken.Type
                              });

                await _navigation.PopToRootAsync();
                

            }
            else
            {
                var page = new ContentPage();
                await page.DisplayAlert("Alerta", "El usuario ingresado no es valido.", "OK");
    }
            
            IsBusy = false;
        },
        () => !string.IsNullOrEmpty(Usuario) && !string.IsNullOrEmpty(Password)));
    }
}