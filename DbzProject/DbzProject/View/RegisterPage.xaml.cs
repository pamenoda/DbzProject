namespace DbzProject.View;

public partial class RegisterPage : ContentPage
{
	private RegisterViewModel viewModel;
	public RegisterPage(RegisterViewModel viewModel)
	{
		this.viewModel = viewModel;
		InitializeComponent();
		BindingContext = viewModel;
	}
}