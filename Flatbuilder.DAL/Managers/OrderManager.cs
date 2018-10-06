using Flatbuilder.DAL.Context;
using Flatbuilder.DAL.Entities;
using Flatbuilder.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flatbuilder.DAL.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly FlatbuilderContext _context;

        public OrderManager(FlatbuilderContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Costumer)
                .Include(o => o.Rooms)
                .ToListAsync();

            return orders;
        }
    }
}
