using System;
using System.Collections.Generic;
using System.Text;

namespace Flatbuilder.DTO
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int OrderId { get; set; }
        public Orders Order { get; set; }
        public string Note { get; set; }
    }
}
