using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Interfaces
{
    interface IXmlGenerate<T>
    {
        void XmlGenerate(IEnumerable<T> info);
    }
}
