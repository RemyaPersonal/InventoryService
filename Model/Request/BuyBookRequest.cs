using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    public class BuyBookRequest
    {
        public int bookId { get; set; }
        public string catagoryId { get; set; }
        public int quantity { get; set; }
    }
}
