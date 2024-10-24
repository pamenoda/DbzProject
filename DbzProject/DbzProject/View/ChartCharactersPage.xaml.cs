namespace DbzProject.View;

public partial class ChartCharactersPage : ContentPage
{
	private ChartCharactersViewModel viewModel;
	public ChartCharactersPage(ChartCharactersViewModel viewModel)
	{
		this.viewModel = viewModel;
		InitializeComponent();
		BindingContext = viewModel;
	}
}