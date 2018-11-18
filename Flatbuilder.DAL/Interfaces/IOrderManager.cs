using Flatbuilder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flatbuilder.DAL.Interfaces
{
    public interface IOrderManager
    {
        Task<List<Order>> GetOrders();
        Task<List<Order>> GetOrdersByName(string name);
        Task<Order> GetOrderById(int id);
        Task DeleteOrder(Order o);
        Task<Order> AddOrder(Order order,List<Room> rooms);
        Task InsertAsync();
        Task SaveChanges();
    }
}
