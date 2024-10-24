
namespace DbzProject.View;

public partial class MainPage : ContentPage
{
	MainViewModel viewModel;
	public MainPage(MainViewModel viewModel)
	{
		this.viewModel = viewModel;
        InitializeComponent();
		BindingContext = viewModel;
	}

   
}