namespace DbzProject.View;

public partial class MainMenuPage : ContentPage
{
	private MainMenuViewModel viewModel;
    public MainMenuPage(MainMenuViewModel viewModel)
	{
		this.viewModel = viewModel;
        InitializeComponent();
		BindingContext = viewModel;
	}

	public void ChangeComPort(object sender, EventArgs e) 
	{
        viewModel.RefreshComSelectedCommand.Execute(this);
    }
}