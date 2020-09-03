using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private ITaxRepository _taxRepository;
        private IProductRepository _productRepository;
        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _taxRepository = new TaxRepository();
            _productRepository = new ProductRepository();
        }
        public OrderLookupResponse LookupOrder(string orderDate)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            var orders =_orderRepository.LoadOrders(orderDate);
            try
            {
                if (orders.Count == 0)
                {
                    response.Success = false;
                    response.Message = $"{orderDate} is not a valid order date.";
                }
                else
                {
                    response.Success = true;
                    response.Date = orderDate;
                    response.Orders = orders;

                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public AddOrderResponse AddOrder(Order order, string date)
        {
            AddOrderResponse response = new AddOrderResponse();
            var orders = _orderRepository.LoadOrders(date);
            var taxes = _taxRepository.LoadTaxRate();
            var products = _productRepository.LoadProductType();

            try
            {
                int totalOrders = 0;
                if (orders.Count > 0)
                {
                    totalOrders = orders.Select(a => a.OrderNumber).Max();
                }
                order.OrderNumber = totalOrders + 1;
                var taxRate = taxes.First(a => a.StateAbbreviation == order.State);
                order.TaxRate = taxRate.TaxRate;
                var productType = products.First(a => a.ProductType == order.ProductType);
                order.CostPerSquareFoot = productType.CostPerSquareFoot;
                order.LaborCostPerSquareFoot = productType.LaborCostPerSquareFoot;
                order = CalculateOrderCosts(order);
                orders.Add(order);
                response.Success = true;
                response.Date = date;
                response.Orders = orders;
                response.Order = order;
                _orderRepository.SaveOrder(orders, date);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        public EditOrderResponse EditOrder(Order order, string date, int orderNumber)
        {
            EditOrderResponse response = new EditOrderResponse();
            var orders = _orderRepository.LoadOrders(date);
            var taxes = _taxRepository.LoadTaxRate();
            var products = _productRepository.LoadProductType();

            try
            {
                var taxRate = taxes.First(a => a.StateAbbreviation == order.State);
                order.TaxRate = taxRate.TaxRate;
                var productType = products.First(a => a.ProductType == order.ProductType);
                order.CostPerSquareFoot = productType.CostPerSquareFoot;
                order.LaborCostPerSquareFoot = productType.LaborCostPerSquareFoot;
                order = CalculateOrderCosts(order);
                var newList = orders.Where(a => a.OrderNumber != orderNumber).ToList();
                newList.Add(order);
                response.Success = true;
                response.Date = date;
                response.Orders = orders;
                response.Order = order;
                _orderRepository.SaveOrder(newList, date);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        public RemoveOrderResponse RemoveOrder(Order order, string date, int orderNumber)
        {
            RemoveOrderResponse response = new RemoveOrderResponse();
            var orders = _orderRepository.LoadOrders(date);
            try
            {
                var newList = orders.Where(a => a.OrderNumber != orderNumber).ToList();
                _orderRepository.SaveOrder(newList, date);
                response.Success = true;
                response.Date = date;
                response.Orders = orders;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        private Order CalculateOrderCosts(Order order)
        {
            var materialCost = order.CostPerSquareFoot * order.Area;
            var laborCost = order.LaborCostPerSquareFoot * order.Area;
            var tax = (materialCost + laborCost) * (order.TaxRate / 100);
            var total = materialCost + laborCost + tax;
            order.MaterialCost = materialCost;
            order.LaborCost = laborCost;
            order.Tax = tax;
            order.Total = total;
            return order;
        }
    }
}
