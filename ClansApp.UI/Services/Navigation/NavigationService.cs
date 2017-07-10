using ClansApp.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using ClansApp.UI.Extensions;

namespace ClansApp.UI.Services.Navigation
{
    /// <summary>
    /// Basic navigation class
    /// </summary>
    /// <typeparam name="TBase">
    /// Base type of objects we want to navigate through - in our case, ViewModels
    /// </typeparam>
    public class NavigationService<TBase> where TBase : class
    {
        /// <summary>
        /// List containing registered types with their settings, i.e. StartPage
        /// </summary>
        private IList<NavigationItem<TBase>> _navigationItems = new List<NavigationItem<TBase>>();
        /// <summary>
        /// Collection containing browsed ViewModels history
        /// </summary>
        private List<TBase> _navigationCollection = new List<TBase>();
        /// <summary>
        /// Main window which contains current ViewModel property
        /// </summary>
        private readonly IWindowFrame<TBase> _windowFrame;

        /// <summary>
        /// Self-explanatory
        /// </summary>
        public ICommand MovePrev { get; private set; }

        public NavigationService(IWindowFrame<TBase> windowFrame)
        {
            _windowFrame = windowFrame;
            // subscribe to PropertyChanged event of INotifyPropertyChanged interface,
            // to get notified on every current navigation object update
            _windowFrame.PropertyChanged += OnCurrentViewModelChanged;

            MovePrev = new RelayCommand<object>(OnMovePrev, OnCanMovePrev);
        }

        #region Private methods
        private void OnCurrentViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentViewModel")
            {
                UpdateNavigationCollection();
            }
        }

        #region MovePrev command delegate methods
        private void OnMovePrev(object obj)
        {
            SetCurrentViewModel(_navigationCollection[_navigationCollection.Count - 2]);
        }
        private bool OnCanMovePrev(object obj)
        {
            return _navigationCollection.Count > 1;
        } 
        #endregion

        /// <summary>
        /// Primary method to update our navigation history collection on every CurrentViewModel update
        /// </summary>
        private void UpdateNavigationCollection()
        {
            // find settings of given ViewModel object
            var navigationItem = _navigationItems.FirstOrDefault(x => x.Item == _windowFrame.CurrentViewModel);
            if (navigationItem == null)
            {
                throw new KeyNotFoundException("Given ViewModel is not registered!");
            }

            // look for existing object in our list
            if (_navigationCollection.Contains(navigationItem.Item))
            {
                // if it's a StartPage, clear the navigation history (everything except the StartPage)
                if (navigationItem.IsStartPage)
                {
                    _navigationCollection.RemoveAll(x => x != navigationItem.Item);
                }
                else
                {
                    // remove all items after current ViewModel,
                    // i'm not supporting MoveNext feature in current version
                    var indexToRemove = _navigationCollection.IndexOf(navigationItem.Item) + 1;
                    _navigationCollection.RemoveRange(indexToRemove, _navigationCollection.Count - indexToRemove);
                }
            }
            else
            {
                _navigationCollection.Add(navigationItem.Item);
            }
        }

        private void SetCurrentViewModel(TBase currentViewModel)
        {
            _windowFrame.CurrentViewModel = currentViewModel;
        }

        private TBase GetNavByType<TNav>() where TNav : TBase
        {
            return _navigationItems.FirstOrDefault(x => x.Item.GetType() == typeof(TNav)).Item;
        }
        #endregion

        #region Registering ViewModels for navigation
        public INavigationItem<TBase> Register(Func<TBase> itemBuilder)
        {
            var navigationItem = new NavigationItem<TBase> { Item = itemBuilder() };

            _navigationItems.Add(navigationItem);

            return navigationItem;
        }
        public INavigationItem<TBase> Register(TBase item)
        {
            return Register(() => item);
        } 
        #endregion

        public void MoveToStartPage()
        {
            SetCurrentViewModel(_navigationItems.Where(x => x.IsStartPage).Select(x => x.Item).FirstOrDefault());
        }

        /// <summary>
        /// Sets current desired ViewModel
        /// </summary>
        public void Set<TNav>() where TNav : TBase
        {
            SetCurrentViewModel(GetNavByType<TNav>());
        }

        /// <summary>
        /// Gets ViewModel, i.e. for properties purposes
        /// </summary>
        /// <returns>Instance of desired type</returns>
        public TNav Get<TNav>() where TNav : TBase
        {
            return (TNav)GetNavByType<TNav>();
        }
    }
}
