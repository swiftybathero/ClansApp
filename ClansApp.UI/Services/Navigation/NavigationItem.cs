namespace ClansApp.UI.Services.Navigation
{
    /// <summary>
    /// Settings class for out navigation items
    /// </summary>
    /// <typeparam name="TBase">
    /// Base type of navigation objects
    /// </typeparam>
    public class NavigationItem<TBase> : INavigationItem<TBase> where TBase : class
    {
        public bool IsStartPage { get; set; }
        public TBase Item { get; set; }

        public INavigationItem<TBase> AsStartPage()
        {
            IsStartPage = true;
            return this;
        }
    }
}
