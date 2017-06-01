// ----------------------------------------------------------------------------------------------
// <copyright file="UsuarioDTO.cs" company="SCI Software">
//     Copyright (c) SCI Software 2016. Todos los derechos reservados.
// </copyright>
// <project>Amezquita.ControlTiempos.Mobile</project>
// ----------------------------------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Amezquita.ControlTiempos.Mobile.Features.Accounts
{
    public class UsuarioDTO
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("Cedula")]
        public string Cedula { get; set; }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }
    }
}