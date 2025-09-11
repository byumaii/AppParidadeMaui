using System.Text.Json;
using Trabalho_Palmuti.Models;

namespace Trabalho_Palmuti.Services
{
    public class IbgeService
    {
        private readonly HttpClient _httpClient;

        public IbgeService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Estado> GetEstadoAsync(int estadoId)
        {
            var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{estadoId}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();

                var estado = JsonSerializer.Deserialize<Estado>(jsonContent);
                return estado;
            }

            return null;
        }
    }
}