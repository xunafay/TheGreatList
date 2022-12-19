namespace TheGreatList.Views;

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TheGreatList.Models;
using TheGreatList.Services;

public class EditTaskViewModel : ViewModelBase
{
    private IApiAdapter Api = DependencyService.Get<IApiAdapter>();
    public ObservableCollection<Category> CategoriesList { get; set; } = new ObservableCollection<Category>();
    public String TaskName { get; set; }
    public String TaskDescription { get; set; }

    public Models.Task Task { get; set; }
    public Command Save { get; }
    public Command Delete { get; }
    private EditTask Page;
    private bool Deletable = false;

    public EditTaskViewModel(Models.Task task, EditTask page)
    {
        Task = task;
        TaskName = task.Name;
        TaskDescription = task.Description;
        _ = GetCategoriesAsync();
        Deletable = true;
        Save = new Command(async () =>
        {
            Task.Name = TaskName;
            Task.Description = TaskDescription;
            TaskName = "";
            TaskDescription = "";
            await Api.UpdateTaskAsync(Task);
            //await Page.Navigation.PopAsync();
        });
        Delete = new Command(async () => {
            try
            {
                await Api.DeleteTaskAsync(Task.Id);
            } finally
            {
                // await Page.Navigation.PopAsync();
            }
        });
        
    }

    public EditTaskViewModel(EditTask page)
    {
        Deletable = false;
        Page = page;
        _ = GetCategoriesAsync();
        Save = new Command(async () =>
        {

            var task = new Models.Task
            {
                Name = TaskName,
                Description = TaskDescription,
                Id = 0,
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
            };
            await Api.AddTaskAsync(task);
            TaskName = "";
            TaskDescription = "";
            // Throws an exception
            //await Page.Navigation.PopAsync();
        });
    }

    public async System.Threading.Tasks.Task GetCategoriesAsync()
    {
        var categories = await Api.GetCategoriesAsync();
        CategoriesList.Clear();
        foreach (var category in categories)
        {
            CategoriesList.Add(category);
        }
    }
}

public partial class EditTask : ContentPage
{
    public EditTask()
    {
        InitializeComponent();
        BindingContext = new EditTaskViewModel(this);
    }

    public EditTask(Models.Task task)
    {
        InitializeComponent();
        BindingContext = new EditTaskViewModel(task, this);
    }
}