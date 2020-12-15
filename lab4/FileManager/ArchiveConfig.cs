using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileManager
{
    public class ArchiveConfig
    {
        [JsonPropertyName("extension")]
        [XmlElement(ElementName = "extension")]
        public string Extension { get; set; }

        public ArchiveConfig()
        { }

        public void Compress(string source, string compressed)
        {
            using (FileStream sourceStream = new FileStream(source, FileMode.Open))
            {
                using (FileStream targetStream = File.Create(compressed))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        public void Decompress(string compressed, string target)
        {
            using (FileStream sourceStream = new FileStream(compressed, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(target))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }
        }
    }
}
