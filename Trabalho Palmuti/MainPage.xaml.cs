using Trabalho_Palmuti.Models;
using Trabalho_Palmuti.ViewModels;

namespace Trabalho_Palmuti;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = (MainViewModel)BindingContext;
        await viewModel.InicializarAsync();

        if (AppMetrics.ColdStartStopwatch.IsRunning)
        {
            AppMetrics.ColdStartStopwatch.Stop();
            long elapsedMilliseconds = AppMetrics.ColdStartStopwatch.ElapsedMilliseconds;
            Console.WriteLine($"[AppMetrics] Cold start time: {elapsedMilliseconds} ms");
        }
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = (MainViewModel)BindingContext;
        viewModel.FiltrarCapitais(e.NewTextValue);
    }
    private void OnSearchBarUnfocused(object sender, FocusEventArgs e)
    {
        var viewModel = (MainViewModel)BindingContext;
        viewModel.IsListVisible = false;
    }
    private void OnSearchBarFocused(object sender, FocusEventArgs e)
    {
        var viewModel = (MainViewModel)BindingContext;
        viewModel.IsListVisible = true;
    }
    private async void OnCapitalTapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Capital capitalSelecionada)
        {
            var viewModel = (MainViewModel)BindingContext;
            double distanciaKm = 0;

            if (viewModel.LocalizacaoAtual != null)
            {
                var localizacaoCapital = new Location(capitalSelecionada.Latitude, capitalSelecionada.Longitude);
                distanciaKm = Location.CalculateDistance(viewModel.LocalizacaoAtual, localizacaoCapital, DistanceUnits.Kilometers);

                await DisplayAlert("Distância",
                                   $"A distância até {capitalSelecionada.Nome} é de aproximadamente {distanciaKm:F0} km.",
                                   "OK");
            }
            else
            {
                await DisplayAlert("Aviso", "Clique em 'Obter Localização Atual' primeiro para podermos calcular a distância.", "OK");
            }
            var parametros = new Dictionary<string, object>
        {
            { "Capital", capitalSelecionada },
            { "Distancia", distanciaKm }
        };
            await Shell.Current.GoToAsync(nameof(EstadoDetailPage), parametros);
        }
    }
    private async void OnLocationClicked(object sender, EventArgs e)
    {
        try
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Location location = await Geolocation.Default.GetLocationAsync();
            watch.Stop();
            Console.WriteLine($"[AppMetrics] Tempo para obter localização GPS: {watch.ElapsedMilliseconds} ms");

            if (location != null)
            {
                var viewModel = (MainViewModel)BindingContext;
                viewModel.LocalizacaoAtual = location;

                await DisplayAlert("Sucesso", "Sua localização foi salva! Agora clique em uma capital para ver a distância.", "OK");
            }
        }
        catch (FeatureNotSupportedException)
        {
            await DisplayAlert("Erro", "Seu dispositivo não suporta a geolocalização.", "OK");
        }
        catch (PermissionException)
        {
            await DisplayAlert("Erro", "A permissão para acessar a localização foi negada.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro ao obter a localização: {ex.Message}", "OK");
        }
    }
}
      