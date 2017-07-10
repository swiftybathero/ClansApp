namespace ClansApp.UI.Services.Navigation
{
    public interface INavigationItem<TBase> where TBase : class
    {
        INavigationItem<TBase> AsStartPage();
    }
}
