using TheGreatList.Models;
using TheGreatList.Services;

namespace TheGreatList.Views;

public class EditCategoryViewModel : ViewModelBase
{
    private IApiAdapter Api = DependencyService.Get<IApiAdapter>();
    public String CategoryName { get; set; }
    public String CategoryDescription { get; set; }

    public Category Category { get; set; }
    public Command Save { get; }
    public Command Delete { get; }
    private EditCategory Page;
    private bool Deletable = false;

    public EditCategoryViewModel(Category category, EditCategory page)
    {
        Category = category;
        CategoryName = category.Name;
        CategoryDescription = category.Description;
        Save = new Command(async () =>
        {
            Category.Name = CategoryName;
            Category.Description = CategoryDescription;
            CategoryName = "";
            CategoryDescription = "";
            await Api.UpdateCategoryAsync(Category);
            // await Page.Navigation.PopAsync();
        });
        Delete = new Command(async () =>
        {
            try
            {
                await Api.DeleteCategoryAsync(Category.Id);
            }
            finally
            {
                //await Page.Navigation.PopAsync();
            }
        });
        Deletable = true;
    }

    public EditCategoryViewModel(EditCategory page)
    {
        Deletable = false;
        Page = page;
        Save = new Command(async () =>
        {

            var category = new Category
            {
                Name = CategoryName,
                Description = CategoryDescription,
                Id = 0,
            };
            await Api.AddCategoryAsync(category);
            CategoryName = "";
            CategoryDescription = "";
            // await Page.Navigation.PopAsync();
        });
    }
}

public partial class EditCategory : ContentPage
{
    public EditCategory()
    {
        InitializeComponent();
        BindingContext = new EditCategoryViewModel(this);
    }

    public EditCategory(Category category)
    {
        InitializeComponent();
        BindingContext = new EditCategoryViewModel(category, this);
    }
}