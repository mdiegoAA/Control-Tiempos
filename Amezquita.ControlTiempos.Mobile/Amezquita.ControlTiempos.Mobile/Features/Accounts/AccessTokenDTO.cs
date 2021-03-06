﻿using Newtonsoft.Json;

namespace Amezquita.ControlTiempos.Mobile.Features.Accounts
{
    public class AccessTokenDTO
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("token_type")]
        public string Type { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
