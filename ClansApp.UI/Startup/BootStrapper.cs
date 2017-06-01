using Autofac;
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
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<WindowFrame>().AsSelf();
            builder.RegisterType<WindowFrameViewModel>().AsSelf();

            builder.RegisterType<ClansDataService>().As<IClansDataService>();
            builder.RegisterType<XmlSettingsSerializer>().As<ISettingsSerializer>();

            return builder.Build();
        }
    }
}
