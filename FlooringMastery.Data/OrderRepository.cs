using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using System.IO;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data
{
    public class OrderRepository : IOrderRepository
    {
        private string path = @"C:\Users\John\Desktop\SoftwareGuild\C#\Badge 2\Milestone 2\Flooring Mastery\Orders\";
        public List<Order> LoadOrders(string date)
        {
            List<Order> Orders = new List<Order>();
            string filename = "Orders_" + date + ".txt";
            var fileToRead = path + filename;
            if (File.Exists(fileToRead))
            {
                var reader = File.ReadAllLines(fileToRead);
                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');
                    var order = new Order();

                    order.OrderNumber = int.Parse(columns[0]);
                    order.CustomerName = columns[1];
                    order.State = columns[2];
                    order.TaxRate = decimal.Parse(columns[3]);
                    order.ProductType = columns[4];
                    order.Area = decimal.Parse(columns[5]);
                    order.CostPerSquareFoot = decimal.Parse(columns[6]);
                    order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                    order.MaterialCost = decimal.Parse(columns[8]);
                    order.LaborCost = decimal.Parse(columns[9]);
                    order.Tax = decimal.Parse(columns[10]);
                    order.Total = decimal.Parse(columns[11]);
                    order.Date = date;
                    Orders.Add(order);
                }
            }
            return Orders;
        }
        public void SaveOrder(List<Order> orders, string date)
        {
            string filename = "Orders_" + date + ".txt";
            var fileToRead = path + filename;
            if (File.Exists(fileToRead))
            {
                File.Delete(fileToRead);
            }
            using (var writer = File.CreateText(fileToRead))
            {
                writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                foreach (var order in orders)
                {
                    writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", order.OrderNumber, order.CustomerName, order.State, order.Tax,
                        order.ProductType, order.Area, order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);

                }
            }
        }
    }
}
