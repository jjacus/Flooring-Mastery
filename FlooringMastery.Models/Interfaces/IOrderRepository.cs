using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> LoadOrders(string OrderDate);
        void SaveOrder(List<Order> orders, string date);

    }
}
