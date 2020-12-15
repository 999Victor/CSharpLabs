using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManager
{
    public class ConfigManager
    {
        public ConfigManager()
        { }

        public T GetConfig<T>(string path) where T : new()
        {
            T config = new T();

            switch (Path.GetExtension(path))
            {
                case ".xml":
                    var xmlParser = new XmlParser();
                    config = xmlParser.Parse<T>(path);
                    break;
                case ".json":
                    var jsonParser = new JsonParser();
                    config = jsonParser.Parse<T>(path);
                    break;
                default:
                    throw new IOException("Файл с неправильным расширением");
            }

            return config;
        }
    }
}
