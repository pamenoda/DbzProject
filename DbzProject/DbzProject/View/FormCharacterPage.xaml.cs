namespace DbzProject.View;

public partial class FormCharacterPage : ContentPage
{
	private FormCharacterViewModel viewModel;
	public FormCharacterPage(FormCharacterViewModel viewModel)
	{
		this.viewModel = viewModel;
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnDisappearing()
    {
        viewModel.CloseScannerCommand.Execute(this);
    }
}