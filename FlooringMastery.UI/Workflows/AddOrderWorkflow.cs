using FlooringMastery.BLL;
using FlooringMastery.UI;
using FlooringMastery.Models;
using FlooringMastery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.UI.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderManager orderManager = OrderManagerFactory.Create();

            string date = GetDate();
            string name = GetName();
            string state = GetState();
            decimal area = GetArea();
            string product = GetProduct();

            Order order = new Order();
            order.CustomerName = name;
            order.State = state;
            order.Area = area;
            order.ProductType = product;

            AddOrderResponse response = orderManager.AddOrder(order, date);
            ConsoleIO.PreviewNewOrder(response.Order);
            var newOrder = response.Order;
            while(true)
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
            string GetName()
            {
                while(true)
                {
                    Console.Write("Enter a customer name:");
                    string input = Console.ReadLine();

                    if (input.Length > 0 && input != "")
                    {
                        return input;
                    }
                }
            }
            string GetState()
            {
                while(true)
                {
                    Console.Write("Enter a state name (OH/PA/MI/IN):");
                    string input = Console.ReadLine();
                    TaxRepository tax = new TaxRepository();
                    var taxes = tax.LoadTaxRate();
                    foreach (var item in taxes)
                    {
                        if (input.ToUpper() == item.StateAbbreviation)
                        {
                            return input.ToUpper();
                        }
                    }
                }
            }
            decimal GetArea()
            {
                while (true)
                {
                    Console.Write("Enter an area greater than 0 and less than 100: ");
                    string input = Console.ReadLine();
                    decimal num;
                    bool test = decimal.TryParse(input, out num);
                    if (test)
                    {
                        if (int.Parse(input) > 0 && int.Parse(input) < 100)
                        {
                            return int.Parse(input);
                        }
                    }
                }
            }
            string GetProduct()
            {
                while(true)
                {
                    Console.Write("Enter product type (only carpet/laminate/tile/wood): ");
                    string input = Console.ReadLine();
                    ProductRepository repo = new ProductRepository();
                    var products = repo.LoadProductType();
                    foreach (var item in products)
                    {
                        if (input.ToUpper() == item.ProductType)
                        {
                            return input.ToUpper();
                        }
                    }
                }
            }
        }
    }
}
