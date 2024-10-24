namespace DbzProject.View;

public partial class ShowCharacterPage : ContentPage
{
	private ShowCharacterViewModel viewModel;
	public ShowCharacterPage(ShowCharacterViewModel viewModel)
	{
		this.viewModel = viewModel;	
		InitializeComponent();
		BindingContext = viewModel;
	}
}