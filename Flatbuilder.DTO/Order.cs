using System;
using System.Collections.Generic;

namespace Flatbuilder.DTO
{
    //Already made orders
    public class Order
    {
        public int Id { get; set; }
        public int CostumerId { get; set; }
        public Costumer Costumer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public ICollection<OrderRoom> OrderRooms { get; set; }

        public Order()
        {
            OrderRooms = new List<OrderRoom>();
        }
    }
}
