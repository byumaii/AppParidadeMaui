using CommunityToolkit.Mvvm.ComponentModel;
using Trabalho_Palmuti.Models;
using Trabalho_Palmuti.Services;

namespace Trabalho_Palmuti.ViewModels
{
    [QueryProperty(nameof(Capital), "Capital")]
    public partial class EstadoDetailViewModel : ObservableObject
    {
        private readonly IbgeService _ibgeService;

        [ObservableProperty]
        private Capital capital;

        [ObservableProperty]
        private Estado estado;

        [ObservableProperty]
        private bool isLoading;

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