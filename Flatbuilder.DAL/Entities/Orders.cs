using System;
using System.Collections.Generic;

namespace Flatbuilder.DTO
{
    //Already made orders
    public class Orders
    {
        public Costumer costumer;
        public DateTime startDate;
        public DateTime endDate;
        public String location;
        public ICollection<OrderItem> rooms;
    }
}
