using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.BLL;
using FlooringMastery.UI;
using FlooringMastery.Data;

namespace FlooringMastery.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void AddOrderTest()
        {
            OrderManager orderManager = OrderManagerFactory.Create();
            Order order = new Order();
            order.OrderNumber = 2;
            order.CustomerName = "John";
            order.State = "PA";
            order.TaxRate = 6.75M;
            order.ProductType = "CARPET";
            order.Area = 10M;
            order.CostPerSquareFoot = 2.25M;
            order.LaborCostPerSquareFoot = 2.10M;
            order.MaterialCost = 22.5M;
            order.LaborCost = 21.0M;
            order.Tax = 2.93625M;
            order.Total = 46.43625M;
            string date = "10101990";
            var response = orderManager.AddOrder(order, date);
            var count = response.Orders.Count;

            Assert.AreEqual(count, 2);
        }

        [Test]
        public void EditOrderTest()
        {
            OrderManager orderManager = OrderManagerFactory.Create();
            Order order = new Order();
            order.OrderNumber = 1;
            order.CustomerName = "John";
            order.State = "PA";
            order.TaxRate = 6.75M;
            order.ProductType = "CARPET";
            order.Area = 10M;
            order.CostPerSquareFoot = 2.25M;
            order.LaborCostPerSquareFoot = 2.10M;
            order.MaterialCost = 22.5M;
            order.LaborCost = 21.0M;
            order.Tax = 2.93625M;
            order.Total = 46.43625M;
            string date = "10101990";
            var response = orderManager.EditOrder(order, date, 1);

            Assert.AreEqual(order, response.Order);

        }
    }
}
