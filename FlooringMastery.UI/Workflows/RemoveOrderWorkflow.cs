using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;

namespace FlooringMastery.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            OrderManager orderManager = OrderManagerFactory.Create();
            string date = GetDate();
            int orderNumber = GetOrderNumber();
            var response = orderManager.LookupOrder(date);
            if (response.Orders == null)
            {
                Console.WriteLine("No order exists with that number on that date.");
                Console.ReadKey();
            }
            else
            {
                var order = response.Orders.FirstOrDefault(a => a.OrderNumber == orderNumber);
                ConsoleIO.PreviewNewOrder(order);
                var newOrder = order;
                while (true)
                {
                    Console.WriteLine("Does this information look correct and would you like to remove the order? (Y/N)");
                    string input = Console.ReadLine();
                    if (input.ToUpper() == "Y")
                    {
                        orderManager.RemoveOrder(newOrder, date, newOrder.OrderNumber);
                        Console.Write("Order has been cancelled.");
                        Console.ReadKey();
                        return;
                    }
                    if (input.ToUpper() == "N")
                    {
                        Console.Write("Order added successfully.");
                        Console.ReadKey();
                        return;
                    }
                }
            }

            int GetOrderNumber()
            {
                while (true)
                {
                    Console.Write("Enter an order number:");
                    string input = Console.ReadLine();
                    bool parsedInput = int.TryParse(input, out int num);
                    if (parsedInput)
                    {
                        return num;
                    }
                }
            }
            string GetDate()
            {
                while (true)
                {
                    Console.Write("Enter a date for your order(dd/MM/yyyy):");
                    string input = Console.ReadLine();
                    DateTime currentDate = DateTime.Now;
                    DateTime dateCheck;
                    bool parsedDateTime = DateTime.TryParse(input, out dateCheck);

                    if (parsedDateTime)
                    {
                        if (dateCheck < currentDate)
                        {
                            Console.WriteLine("Orders must be placed in the future");
                        }
                        else
                        {
                            return dateCheck.ToString("ddMMyyyy");
                        }
                    }
                    Console.ReadKey();
                }
            }
        }
    }
}
