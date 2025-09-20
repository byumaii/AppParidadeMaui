using System.ComponentModel;
using Trabalho_Palmuti.Models;
using Trabalho_Palmuti.Services;
using System.Diagnostics;

namespace Trabalho_Palmuti;

[QueryProperty(nameof(Capital), "Capital")]
[QueryProperty(nameof(Distancia), "Distancia")]
public partial class EstadoDetailPage : ContentPage, INotifyPropertyChanged
{
    private Capital _capital;
    private Estado _estado;
    private double _distancia;
    private string _distanciaFormatada;
    private readonly IbgeService _ibgeService;

    public Capital Capital
    {
        get => _capital;
        set { _capital = value; OnPropertyChanged();_=CarregarDetalhesDoEstadoAsync(); }
    }

    public Estado Estado
    {
        get => _estado;
        set { _estado = value; OnPropertyChanged(); }
    }

    public double Distancia
    {
        get => _distancia;
        set
        {
            _distancia = value;
            if (value > 0)
            {
                DistanciaFormatada = $"Distância até a sua localização: {value:F0} km";
            }
            else
            {
                DistanciaFormatada = "Clique em 'Obter Localização' na tela anterior para ver a distância.";
            }
        }
    }
    public string DistanciaFormatada
    {
        get => _distanciaFormatada;
        set { _distanciaFormatada = value; OnPropertyChanged(); }
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
            var watch = Stopwatch.StartNew();
            var estadoDaApi = await _ibgeService.GetEstadoAsync(Capital.EstadoId);
            watch.Stop();
            Console.WriteLine($"[AppMetrics] Tempo da chamada à API do IBGE: {watch.ElapsedMilliseconds} ms");
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