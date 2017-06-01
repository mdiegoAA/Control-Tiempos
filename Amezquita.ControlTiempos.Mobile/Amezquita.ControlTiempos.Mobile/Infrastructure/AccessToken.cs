using SQLite;

namespace Amezquita.ControlTiempos.Mobile.Infrastructure
{
    public class AccessToken
    {
        [PrimaryKey]
        public string Token { get; set; }
        public string Type { get; set; }
        public int ExpiresIn { get; set; }
    }
}