using System;
using System.Collections.Generic;
using System.Text;

namespace Flatbuilder.DAL.Entities
{
    //generalization of all the rooms
    public class Room
    {
        public int Id { get; set; }       
        public double Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null;
    }
}
