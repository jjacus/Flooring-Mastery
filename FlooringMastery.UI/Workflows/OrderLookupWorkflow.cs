using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.UI.Workflows
{
    public class OrderLookupWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Lookup an order");
            Console.WriteLine("***********************");
            Console.Write("Enter an order date:");
            string input = Console.ReadLine();

            OrderLookupResponse response = manager.LookupOrder(input);

            if(response.Success)
            {
                ConsoleIO.DisplayOrderDetails(response);
            }
            else
            {
                Console.WriteLine("An Error occurred: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
