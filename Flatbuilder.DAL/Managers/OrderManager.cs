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
            var kitchen1 = new Kitchen();
            kitchen1.Price = 400;
            var bedroom1 = new Bedroom();
            bedroom1.Price = 200;
            var shower1 = new Shower();
            var costumer1 = new Costumer();
            var order1 = new Order();
            order1.Costumer = costumer1;
            order1.StartDate = DateTime.Now;
            order1.EndDate = DateTime.Now;
            order1.Location = "loc1";
            order1.Rooms.Add(kitchen1);
            kitchen1.Order = order1;
            order1.Rooms.Add(bedroom1);
            bedroom1.Order = order1;
            var order2 = new Order();
            order2.Rooms.Add(shower1);
            order2.Costumer = costumer1;
            shower1.Order = order2;
            _context.Rooms.Add(kitchen1);
            _context.Rooms.Add(bedroom1);
            _context.Rooms.Add(shower1);
            _context.Costumers.Add(costumer1);
            _context.Orders.Add(order1);
            _context.Orders.Add(order2);

            await _context.SaveChangesAsync();

            var orders = await _context.Orders
                .Include(o => o.Costumer)
                .Include(o => o.Rooms)
                .ToListAsync();

            return orders;
        }
    }
}
