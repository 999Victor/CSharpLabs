using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Products
    {
        public int Id { get; set; }
        public string ProducName { get; set; }
        public string ProducNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int ReorderPoint { get; set; }
        public DateTime? Date { get; set; }
    }
}
