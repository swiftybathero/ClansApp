using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClansApp.UI.Serializers
{
    public interface ISettingsSerializer
    {
        void SaveAPIKey(string value);
        string LoadAPIKey();
    }
}
