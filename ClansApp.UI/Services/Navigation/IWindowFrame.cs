using System.ComponentModel;

namespace ClansApp.UI.Services.Navigation
{
    /// <summary>
    /// Base interface to let navigation service handle nav on WindowFrameViewModel
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    public interface IWindowFrame<TBase> : INotifyPropertyChanged where TBase : class
    {
        TBase CurrentViewModel { get; set; }
    }
}
