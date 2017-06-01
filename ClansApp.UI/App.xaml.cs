using Autofac;
using ClansApp.UI.Startup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClansApp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Build(); 
        }

        public void Build()
        {
            var container = new BootStrapper().BootStrap();

            var windowFrame = container.Resolve<WindowFrame>();
            windowFrame.Show();
        }
    }
}
