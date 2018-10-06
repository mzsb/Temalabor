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
        Task InsertAsync();
    }
}
