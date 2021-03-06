﻿
using ClansApp.UI.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.UI.ViewModels
{
    /// <summary>
    /// Basic class for ViewModels
    /// </summary>
    public abstract class ViewModelBase : Observable
    {
        /// <summary>
        /// Wrapper for property setter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member">Property member value</param>
        /// <param name="value">Value to set</param>
        /// <param name="propertyName">Name of the property</param>
        protected void SetProperty<T>(ref T member, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(member, value))
            {
                return;
            }
            member = value;
            OnPropertyChanged(propertyName);
        }
    }
}
