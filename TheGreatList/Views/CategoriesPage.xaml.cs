using System.Collections.ObjectModel;
using TheGreatList.Models;
using TheGreatList.Services;

namespace TheGreatList.Views;

public class CategoriesViewModel : ViewModelBase
{
    private IApiAdapter Api = DependencyService.Get<IApiAdapter>();
    private CategoriesPage Page;

    public ObservableCollection<Category> CategoriesList { get; set; } = new ObservableCollection<Category>();
    private Category _selectedCategory;
    public Category SelectedCategory
    {
        get { return _selectedCategory; }
        set
        {
            _selectedCategory = value;
            OnPropertyChanged();
        }
    }

    public Command SelectionChanged { get; }
    public Command StartRefresh { get; }
    public Command AddCategory { get; }

    bool _refreshing = false;
    public bool Refreshing
    {
        get { return _refreshing; }
        set
        {
            _refreshing = value;
            OnPropertyChanged();
        }
    }

    public CategoriesViewModel(CategoriesPage page)
    {
        Page = page;
        SelectionChanged = new Command(() =>
        {
            Page.Navigation.PushAsync(new EditCategory(SelectedCategory));
        });
        StartRefresh = new Command(async () =>
        {
            Refreshing = true;
            await Refresh();
            Refreshing = false;
        });
        AddCategory = new Command(() =>
        {
            Page.Navigation.PushAsync(new EditCategory());
        });
    }

    public async System.Threading.Tasks.Task Refresh()
    {
        var categories = await Api.GetCategoriesAsync();
        CategoriesList.Clear();
        foreach (var category in categories)
        {
            CategoriesList.Add(category);
        }
    }
}

public partial class CategoriesPage : ContentPage
{
    public CategoriesPage()
    {
        InitializeComponent();
        BindingContext = new CategoriesViewModel(this);
        Appearing += async (sender, e) => await (BindingContext as CategoriesViewModel).Refresh();
    }
}