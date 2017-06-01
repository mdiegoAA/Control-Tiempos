using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Amezquita.ControlTiempos.Mobile.Infrastructure
{
    public interface IHttpService
    {
        Task<TResult> PostAsync<TResult>(string url, ByteArrayContent body, AuthenticationHeaderValue auth = null);

        Task<TResult> GetAsync<TResult>(string url, AuthenticationHeaderValue auth = null);
    }
}