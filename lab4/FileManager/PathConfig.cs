using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DirectoryMonitorService
{
    public class PathConfig : IValidatableObject
    {
        [JsonPropertyName("sourceDir")]
        [XmlElement(ElementName = "sourceDir")]
        public string Source { get; set; }

        [JsonPropertyName("targetDir")]
        [XmlElement(ElementName = "targetDir")]
        public string Target { get; set; }

        [JsonPropertyName("PathToDB1")]
        [XmlElement(ElementName = "PathToDB1")]
        public string PathToDB { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (!Directory.Exists(this.Source) || !Directory.Exists(this.Target))
                errors.Add(new ValidationResult("Неверный путь к файлу Log"));

            return errors;
        }
    }
}
