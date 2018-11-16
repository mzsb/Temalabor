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
    public class RoomManager : IRoomManager
    {
        private readonly FlatbuilderContext _context;

        public RoomManager(FlatbuilderContext context)
        {
            _context = context;
        }


        public async Task<Room> GetRoomById(int id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(o => o.Id == id);

            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _context.Rooms
                .AsNoTracking()
                .ToListAsync();
            return rooms;
        }

        public async Task AddRoom(Room r)
        {
            await _context.Rooms.AddAsync(r);
            await SaveChanges();
        }

        public async Task DeletRoom(Room r)
        {
            _context.Remove(r);

            await SaveChanges();
        }

        public async Task<Room> UpdateRoom(int id, Room r)
        {
            var update = await GetRoomById(id);

            if (update == null)
                return null;

            update.Price = r.Price;

            await SaveChanges();

            return update;
        }

        public async Task SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception("Concurrency error");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}