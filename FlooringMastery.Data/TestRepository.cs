using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models;
using FlooringMastery.Data;

namespace FlooringMastery.Data
{
    public class TestRepository : IOrderRepository
    {
        private static List<Order> _orders;

        public TestRepository()
        {
            if (_orders == null)
            {
                _orders = new List<Order>();
                var order = new Order();
                order.OrderNumber = 1;
                order.CustomerName = "John";
                order.State = "PA";
                order.TaxRate = 6.75M;
                order.ProductType = "Carpet";
                order.Area = 10M;
                order.CostPerSquareFoot = 2.25M;
                order.LaborCostPerSquareFoot = 2.10M;
                order.MaterialCost = 22.5M;
                order.LaborCost = 21.0M;
                order.Tax = 2.93625M;
                order.Total = 46.43625M;
                order.Date = "10101990";
                _orders.Add(order);
            }
        }

        public List<Order> LoadOrders(string date)
        {
            return _orders;
        }

        public void SaveOrder(List<Order> orders, string date)
        {
            _orders = orders;
        }
    }
}
