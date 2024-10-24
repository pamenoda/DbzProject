namespace DbzProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainMenuPage),typeof(MainMenuPage));
            Routing.RegisterRoute(nameof(FormCharacterPage),typeof(FormCharacterPage));
            Routing.RegisterRoute(nameof(ShowCharacterPage),typeof(ShowCharacterPage));
            Routing.RegisterRoute(nameof(ChartCharactersPage),typeof(ChartCharactersPage));
            Routing.RegisterRoute(nameof(CharactersCSVExportPage),typeof(CharactersCSVExportPage));
            Routing.RegisterRoute(nameof(RegisterPage),typeof(RegisterPage));
        }
    }
}
