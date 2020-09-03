using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public class ConsoleIO
    {
        public static void DisplayOrderDetails(OrderLookupResponse response)
        {
            foreach (var order in response.Orders)
            {
                Console.WriteLine("***********************************");
                Console.WriteLine($"{order.OrderNumber} | {order.Date}");
                Console.WriteLine($"{order.CustomerName}");
                Console.WriteLine($"{order.State}");
                Console.WriteLine($"Product : {order.ProductType}");
                Console.WriteLine($"Materials : {order.MaterialCost}");
                Console.WriteLine($"Labor : {order.LaborCost}");
                Console.WriteLine($"Tax : {order.Tax}");
                Console.WriteLine($"Total : {order.Total}");
                Console.WriteLine("***********************************");
            }
        }
        public static void PreviewNewOrder(Order order)
        {
                Console.WriteLine("***********************************");
                Console.WriteLine($"{order.OrderNumber} | {order.Date}");
                Console.WriteLine($"{order.CustomerName}");
                Console.WriteLine($"{order.State}");
                Console.WriteLine($"Product : {order.ProductType}");
                Console.WriteLine($"Materials : {order.MaterialCost}");
                Console.WriteLine($"Labor : {order.LaborCost}");
                Console.WriteLine($"Tax : {order.Tax}");
                Console.WriteLine($"Total : {order.Total}");
                Console.WriteLine("***********************************");
        }
    }
}
