using System.ComponentModel;
using Trabalho_Palmuti.Models;
using Trabalho_Palmuti.Services;

namespace Trabalho_Palmuti;

[QueryProperty(nameof(Capital), "Capital")]
public partial class EstadoDetailPage : ContentPage, INotifyPropertyChanged
{
    private Capital _capital;
    private Estado _estado;
    private readonly IbgeService _ibgeService;

    public Capital Capital
    {
        get => _capital;
        set
        {
            _capital = value;
            OnPropertyChanged();
            CarregarDetalhesDoEstadoAsync();
        }
    }

    public Estado Estado
    {
        get => _estado;
        set
        {
            _estado = value;
            OnPropertyChanged();
        }
    }

    public EstadoDetailPage()
    {
        InitializeComponent();
        _ibgeService = new IbgeService();
        BindingContext = this;
    }

    private async Task CarregarDetalhesDoEstadoAsync()
    {
        if (Capital != null)
        {
            var estadoDaApi = await _ibgeService.GetEstadoAsync(Capital.EstadoId);
            Estado = estadoDaApi;
        }
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}