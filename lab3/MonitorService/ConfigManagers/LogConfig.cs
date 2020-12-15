using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace DirectoryMonitorService
{
    public class LogConfig : IValidatableObject
    {
        [JsonPropertyName("log")]
        [XmlElement(ElementName = "log")]
        public string Log { get; set; }

        public LogConfig()
        { }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (!File.Exists(this.Log))
                errors.Add(new ValidationResult("Error file path"));

            return errors;
        }
    }
}
