using Alura.ListaLeitura.Seguranca;
using Alura.ListaLeitura.WebApp.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.HttpClientes
{
    public class LoginResult
    {
        public bool Succeeded { get; set; }

        public string Token { get; set; }
    }


    public class AuthApiClient
    {
        private readonly HttpClient _httpClient;
        public AuthApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResult> PostLoginAsync(LoginModel model)
        {
            var resposta = await _httpClient.PostAsJsonAsync("login", model);
            resposta.EnsureSuccessStatusCode();

            return new LoginResult
            {
                Succeeded = resposta.IsSuccessStatusCode,
                Token = await resposta.Content.ReadAsStringAsync()
            };
        }

        public async Task PostRegisterAsync(RegisterViewModel model)
        {
            var resposta = await _httpClient.PostAsJsonAsync<RegisterViewModel>("usuarios", model);
            resposta.EnsureSuccessStatusCode();
        }
    }
}
