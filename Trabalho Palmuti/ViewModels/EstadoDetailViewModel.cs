using CommunityToolkit.Mvvm.ComponentModel;
using Trabalho_Palmuti.Models;
using Trabalho_Palmuti.Services;
using System.Threading.Tasks;

namespace Trabalho_Palmuti.ViewModels
{
    public partial class EstadoDetailViewModel : ObservableObject
    {
        private readonly IbgeService _ibgeService;

        [ObservableProperty]
        private Capital capital;

        [ObservableProperty]
        private Estado estado;

        [ObservableProperty]
        private string distanciaFormatada;

        [ObservableProperty]
        private bool isLoading;

        private double _distancia;
        public double Distancia
        {
            get => _distancia;
            set
            {
                if (_distancia != value)
                {
                    _distancia = value;
                    if (value > 0)
                    {
                        DistanciaFormatada = $"Distância da sua localização: {value:F0} km";
                    }
                    else
                    {
                        DistanciaFormatada = "Clique em 'Obter Localização' na tela anterior para ver a distância.";
                    }
                }
            }
        }

        public EstadoDetailViewModel()
        {
            _ibgeService = new IbgeService();
        }

        async partial void OnCapitalChanged(Capital value)
        {
            if (value == null) return;
            IsLoading = true;
            var estadoDaApi = await _ibgeService.GetEstadoAsync(value.EstadoId);
            Estado = estadoDaApi;
            IsLoading = false;
        }
    }
}