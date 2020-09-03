using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class AddOrderResponse
    {
        public Order Order { get; set; }
        public string Date { get; set; }
        public List<Order> Orders { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
