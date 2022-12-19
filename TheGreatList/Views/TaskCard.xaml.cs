using TheGreatList.Services;

namespace TheGreatList.Views;

public partial class TaskCard : ContentView
{
    private IApiAdapter Api = DependencyService.Get<IApiAdapter>();

    public TaskCard()
	{
		InitializeComponent();
	}

    private void CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        (BindingContext as Models.Task).Completed = e.Value;
        Api.UpdateTaskAsync(BindingContext as Models.Task);
    }
}