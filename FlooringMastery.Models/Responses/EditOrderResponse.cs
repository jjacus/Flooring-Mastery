using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class EditOrderResponse
    {
        public string Date { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Order> Orders { get; set;}
        public Order Order { get; set; }
    }
}
