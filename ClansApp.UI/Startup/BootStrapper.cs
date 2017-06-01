using Autofac;
using ClansApp.Data.DataProviders.REST;
using ClansApp.UI.Serializers;
using ClansApp.UI.Services;
using ClansApp.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.UI.Startup
{
    /// <summary>
    /// Class for initializing application dependencies
    /// </summary>
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<WindowFrame>().AsSelf();
            builder.RegisterType<WindowFrameViewModel>().AsSelf();

            builder.RegisterType<ClansDataService>().As<IClansDataService>();
            builder.RegisterType<XmlSettingsSerializer>().As<ISettingsSerializer>();

            // it should be singleton
            builder.Register(x => ClansDataProvider.Default).As<IClansDataProvider>().SingleInstance();

            return builder.Build();
        }
    }
}
