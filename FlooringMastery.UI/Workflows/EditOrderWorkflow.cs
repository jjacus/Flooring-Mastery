using FlooringMastery.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.UI.Workflows
{
    public class EditOrderWorkflow
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
                Console.WriteLine($"Enter a customer name ({order.CustomerName}):");
                string inputName = Console.ReadLine();
                if (inputName != "")
                {
                    order.CustomerName = inputName;
                }
                Console.WriteLine($"Enter a State ({order.State}):");
                string inputState = Console.ReadLine();
                if (inputState != "")
                {
                    TaxRepository tax = new TaxRepository();
                    var taxes = tax.LoadTaxRate();
                    foreach (var item in taxes)
                    {
                        if (inputState.ToUpper() == item.StateAbbreviation)
                        {
                            order.State = inputState.ToUpper();
                        }
                    }
                }
                Console.WriteLine($"Enter a product type ({order.ProductType}):");
                string inputType = Console.ReadLine();
                if (inputType != "")
                {
                    ProductRepository repo = new ProductRepository();
                    var products = repo.LoadProductType();
                    foreach (var item in products)
                    {
                        if (inputType.ToUpper() == item.ProductType)
                        {
                            order.ProductType = inputType.ToUpper();
                        }
                    }
                }
                Console.WriteLine($"Enter an Area ({order.Area}):");
                string inputArea = Console.ReadLine();
                if (inputArea != "")
                {
                    decimal num;
                    bool test = decimal.TryParse(inputArea, out num);
                    if (test)
                    {
                        if (int.Parse(inputArea) > 0 && int.Parse(inputArea) < 100)
                        {
                            order.Area = int.Parse(inputArea);
                        }
                    }
                }
                EditOrderResponse newResponse = orderManager.EditOrder(order, date, orderNumber);
                var newOrder = newResponse.Order;
                ConsoleIO.PreviewNewOrder(newOrder);
                while (true)
                {
                    Console.WriteLine("Does this information look correct and would you like to save the order? (Y/N)");
                    string input = Console.ReadLine();
                    if (input.ToUpper() == "Y")
                    {
                        Console.Write("Order added successfully.");
                        Console.ReadKey();
                        return;
                    }
                    if (input.ToUpper() == "N")
                    {
                        orderManager.RemoveOrder(newOrder, date, newOrder.OrderNumber);
                        Console.Write("Order has been cancelled.");
                        Console.ReadKey();
                        return;
                    }
                }
            }

            int GetOrderNumber()
            {
                while(true)
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
