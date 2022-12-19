using TheGreatList.Services;
using TheGreatList.Views;

namespace TheGreatList;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		DependencyService.Register<ApiAdapter>();
		MainPage = new NavigationPage(new BottomNavigationPage());
	}
}
