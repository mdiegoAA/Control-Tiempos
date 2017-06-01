// ----------------------------------------------------------------------------------------------
// <copyright file="AccountService.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Mobile</project>
// ----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Amezquita.ControlTiempos.Mobile.Infrastructure;

namespace Amezquita.ControlTiempos.Mobile.Features.Accounts
{
    public class AccountService : IAccountService
    {
        #region Readonly & Static Fields

        private readonly IHttpService _http;
        private readonly IStorage _storage;

        #endregion

        #region C'tors

        public AccountService(IHttpService http, IStorage storage)
        {
            if (http == null)
                throw new ArgumentNullException(nameof(http));

            _http = http;

            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            _storage = storage;

        }

        #endregion

        #region Instance Methods

        public Task<AccessTokenDTO> LoginAsync(LoginDTO dto)
        {
            var login = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("grant_type", "password"),
                            new KeyValuePair<string, string>("username", dto.Usuario),
                            new KeyValuePair<string, string>("password", dto.Password)
                        };

            using (var formContent = new FormUrlEncodedContent(login))
            {
                return _http.PostAsync<AccessTokenDTO>("/Tokens", formContent);
            }
        }

        public async Task<UsuarioDTO> ObtenerUsuarioAsync(string userName, string accessTokenType, string accessToken)
        {   
            UsuarioDTO result = _storage.GetFirst<UsuarioDTO>();

            if (result != null)
                return result;

            var _auth = new AuthenticationHeaderValue(accessTokenType, accessToken);
            result = await _http.GetAsync<UsuarioDTO>($"/Usuarios/ObtenerUsuario/{userName}", _auth);

            _storage.Save(result);

            return result;
        }

        #endregion
    }
}
