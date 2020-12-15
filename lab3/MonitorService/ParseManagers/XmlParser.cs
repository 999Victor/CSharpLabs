using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DirectoryMonitorService
{
    class XmlParser : IParse
    {
        public XmlParser()
        { }

        public T Parse<T>(string path) where T : new()
        {
            T config = new T();
            var formatter = new XmlSerializer(typeof(T));

            ValidateXml(Path.ChangeExtension(path, ".xsd"));

            using (var reader = new StreamReader(path))
            {
                var xmlReader = XmlReader.Create(reader);
                xmlReader.ReadToDescendant(typeof(T).Name);

                config = (T)formatter.Deserialize(xmlReader);

                xmlReader.Close();
            }

            return config;
        }

        public static void ValidateXml(string path)
        {
            try
            {
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add(null, path);

                XDocument document = XDocument.Load(Path.ChangeExtension(path, ".xml"));

                document.Validate(schemas, (sender, validationEventArgs) =>
                {
                    Console.WriteLine(validationEventArgs.Message);
                });
            }
            catch (Exception ex)
            {
                using (var writer = new StreamWriter(@"D:\Programming\CSharpLabs\log.txt", true))
                {
                    writer.WriteLine(ex.Message);
                }
            }

        }
    }
}
