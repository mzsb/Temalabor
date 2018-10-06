using Flatbuilder.DAL.Context;
using Flatbuilder.DAL.Entities;
using Flatbuilder.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .Include(o => o.OrderRooms)
                    .ThenInclude(or => or.Room)
                    .AsNoTracking()
                .ToListAsync();
            return orders;

            //var kitchens = _context.Rooms.OfType<Kitchen>().ToList();
            //var zuhayn = _context.Rooms.OfType<Shower>().ToList();
        }

        public async Task InsertAsync(/*Order order*/)
        {
            var room = new Kitchen { Price = 400 };
            _context.Rooms.Add(room);

            _context.Add(new Order
            {
                Costumer = new Costumer { Name = "nevem" },
                StartDate = DateTime.Now.AddDays(-5),
                EndDate = DateTime.Now.AddDays(1),
                OrderRooms = new List<OrderRoom>
                {
                    new OrderRoom { Room = room, Note = "megrendeles" }
                }
            });

            await _context.SaveChangesAsync();
            //var foglalasi_szobak = order.Rooms.Select(r => r.Id).ToList();

            //var marFoglalva = await _context.Orders.AnyAsync(o => o.Rooms.Any(r => foglalasi_szobak.Contains(r.Id)));
            //var roomId = 7;
            //var szabad = _context.Rooms.Where(r => r.Id == roomId && r.OrderRooms.Any(or => or.Order.EndDate < DateTime.Now ))
        }
    }
}
