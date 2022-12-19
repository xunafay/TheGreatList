namespace TheGreatTesting;

using TheGreatList.Views;

public class OverviewTest
{
    [Fact]
    public void SelectionChanged()
    {
        var page = new OverviewPage();
        var viewModel = new OverviewViewModel(page);
        viewModel.SelectionChanged.Execute(null);
        Assert.Equal(page.Navigation.NavigationStack.Last().GetType(), typeof(EditTask));
    }
}