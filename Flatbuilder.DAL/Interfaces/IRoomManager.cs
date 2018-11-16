using Flatbuilder.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flatbuilder.DAL.Interfaces
{
    public interface IRoomManager
    {
        Task<List<Room>> GetRooms();
        Task<Room> GetRoomById(int id);
        Task AddRoom(Room r);
        Task<Room> UpdateRoom(int id, Room r); 
        Task DeletRoom(Room r);
        Task SaveChanges();
    }
}
