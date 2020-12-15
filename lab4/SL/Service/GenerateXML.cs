using Models;
using SL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SL.Service
{
    public class GenerateXML : IXmlGenerate<Products>
    {
        private readonly string path;

        public GenerateXML(string path)
        {
            this.path = path;
        }

        public void XmlGenerate(IEnumerable<Products> info)
        {
            try
            {
                List<Products> emp = new List<Products>(info);

                XmlSerializer formatter = new XmlSerializer(typeof(List<Products>));

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, emp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
