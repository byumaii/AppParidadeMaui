using System.Collections.ObjectModel;
using Trabalho_Palmuti.Models;
using Trabalho_Palmuti.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Trabalho_Palmuti.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<Capital> Capitais { get; set; } = new();
        public readonly List<Capital> _listaCompletaCapitais = new();
        public Location LocalizacaoAtual { get; set; }
        private readonly CapitalService _capitalService;
        [ObservableProperty]
        private bool isListVisible = false;
        public MainViewModel()
        {
            _capitalService = new CapitalService();
        }

        public async Task InicializarAsync()
        {
            try
            {
                var listaDoServico = await _capitalService.GetCapitais();

                if (listaDoServico?.Count > 0)
                {
                    _listaCompletaCapitais.Clear();
                    foreach (var capital in listaDoServico)
                    {
                        _listaCompletaCapitais.Add(capital);
                    }

                    FiltrarCapitais(string.Empty);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Não foi possível carregar as capitais: {ex.Message}", "OK");
            }
        }

        public void FiltrarCapitais(string termoBusca)
        {
            Capitais.Clear();

            if (string.IsNullOrWhiteSpace(termoBusca))
            {
                foreach (var capital in _listaCompletaCapitais)
                {
                    Capitais.Add(capital);
                }
            }
            else
            {
                var capitaisFiltradas = _listaCompletaCapitais.Where(capital =>
                    capital.Nome.Contains(termoBusca, StringComparison.OrdinalIgnoreCase) ||
                    capital.EstadoSigla.Contains(termoBusca, StringComparison.OrdinalIgnoreCase));

                foreach (var capital in capitaisFiltradas)
                {
                    Capitais.Add(capital);
                }
            }
        }
    }
}