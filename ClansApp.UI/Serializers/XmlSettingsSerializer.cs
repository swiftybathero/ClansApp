using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClansApp.UI.Serializers
{
    public class XmlSettingsSerializer : ISettingsSerializer
    {
        private const string SettingFileName = "settings.xml";
        private XDocument _document;

        public XmlSettingsSerializer()
        {
            InitDocument();
        }

        private void InitDocument()
        {
            if (!File.Exists(SettingFileName))
            {
                _document = DefaultDocument();
                _document.Save(SettingFileName);
            }
            else
            {
                _document = XDocument.Load(SettingFileName);
                LoadAPIKey();
            }
        }

        private XDocument DefaultDocument()
        {
            var defaultDocument = new XDocument
            (
                new XElement
                (
                    "Settings",
                    new XElement("APIKey", "")
                )
            );
            
            return defaultDocument;
        }

        public string LoadAPIKey()
        {
            return APIKeyElement().Value;
        }

        public void SaveAPIKey(string value)
        {
            APIKeyElement().Value = value;
            _document.Save(SettingFileName);
        }

        private XElement APIKeyElement()
        {
            var apiKeyElement = _document.Root.Descendants().FirstOrDefault(x => x.Name == "APIKey");
            if (apiKeyElement == null)
            {
                apiKeyElement = new XElement("APIKey", "");
                _document.Root.Add(apiKeyElement);
            }
            return apiKeyElement;
        }
    }
}
