using System.Collections.ObjectModel;
using TheGreatList.Models;
using TheGreatList.Services;

namespace TheGreatList.Views;

public class OverviewViewModel : ViewModelBase
{
    private IApiAdapter Api = DependencyService.Get<IApiAdapter>();
    private OverviewPage Page;

    public ObservableCollection<Models.Task> TasksList { get; set; } = new ObservableCollection<Models.Task>() { new Models.Task() };
    private Models.Task _selectedTask;
    public Models.Task SelectedTask
    {
        get => _selectedTask;
        set
        {
            _selectedTask = value;
            OnPropertyChanged();
        }
    }

    public Command SelectionChanged { get; }

    public Command StartRefresh { get; }

    bool _refreshing = false;
    public bool Refreshing
    {
        get => _refreshing;
        set
        {
            _refreshing = value;
            OnPropertyChanged();
        }
    }


    public OverviewViewModel(OverviewPage page)
    {
        Page = page;
        SelectionChanged = new Command(() =>
        {
            Page.Navigation.PushAsync(new EditTask(SelectedTask));
        });
        StartRefresh = new Command(async () =>
        {
            Refreshing = true;
            await Refresh();
            Refreshing = false;
        });
    }

    public async System.Threading.Tasks.Task Refresh()
    {
        var tasks = await Api.GetTasksAsync();
        TasksList.Clear();
        foreach (var task in tasks)
        {
            TasksList.Add(task);
        }
    }
}

public partial class OverviewPage : ContentPage
{
    public OverviewPage()
    {
        InitializeComponent();
        BindingContext = new OverviewViewModel(this);
        Appearing += async (sender, e) => await (BindingContext as OverviewViewModel).Refresh();
    }
}