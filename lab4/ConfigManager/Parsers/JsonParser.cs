using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationManager
{
    class JsonParser : IParse
    {
        public JsonParser()
        { }

        public T Parse<T>(string path) where T : new()
        {
            T config = new T();

            using (var stream = new StreamReader(path))
            {
                var jsonText = stream.ReadToEnd();

                if (string.IsNullOrWhiteSpace(jsonText))
                {
                    throw new ArgumentNullException("Пустая строка");
                }

                jsonText = SearchConfig(jsonText, typeof(T).Name);
                config = JsonSerializer.Deserialize<T>(jsonText);
            }

            return config;
        }

        private string SearchConfig(string jsonText, string startPos)
        {
            StringBuilder jsonString = new StringBuilder(jsonText);

            jsonString.Remove(0, jsonText.IndexOf(startPos) + startPos.Length + 3);
            jsonText = jsonString.ToString();

            int count = 0;
            int brackets = 0;

            foreach (char symbol in jsonText)
            {
                if (symbol == '{')
                    brackets++;
                if (symbol == '}')
                    brackets--;

                if (brackets == 0)
                    break;

                count++;
            }

            jsonText = jsonText.Substring(0, count + 1);

            return jsonText;
        }
    }
}
