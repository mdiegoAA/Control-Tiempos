using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Mobile.Features.Accounts
{
    public interface IAccountService
    {
        Task<AccessTokenDTO> LoginAsync(LoginDTO dto);
        Task<UsuarioDTO> ObtenerUsuarioAsync(string userName, string accessTokenType, string accessToken);
    }
}