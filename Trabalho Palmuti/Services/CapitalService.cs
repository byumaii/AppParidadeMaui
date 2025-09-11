using System.Text.Json;
using Trabalho_Palmuti.Models;

namespace Trabalho_Palmuti.Services
{
    public class CapitalService
    {
        public async Task<List<Capital>> GetCapitais()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("Dados/capitais.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();

            var listaCapitais = JsonSerializer.Deserialize<List<Capital>>(contents);

            return listaCapitais;
        }
    }
}