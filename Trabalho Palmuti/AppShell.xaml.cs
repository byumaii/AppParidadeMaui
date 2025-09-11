namespace Trabalho_Palmuti
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EstadoDetailPage), typeof(EstadoDetailPage));
        }
    }
}
