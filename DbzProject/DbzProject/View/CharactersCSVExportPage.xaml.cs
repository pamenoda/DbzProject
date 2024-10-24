namespace DbzProject.View;

public partial class CharactersCSVExportPage : ContentPage
{
	CharactersCSVViewModel viewModel;
	public CharactersCSVExportPage(CharactersCSVViewModel viewModel)
	{
		this.viewModel = viewModel;
        InitializeComponent();
		BindingContext = viewModel;
	}
}