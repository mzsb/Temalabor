using Flatbuilder.DTO;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fb.MC.Views
{
    class DetailsPageModel : FreshBasePageModel
    {
        private Order order;
        public Order Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
                RaisePropertyChanged("Order");
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Order = (Order)initData;
            foreach(Room r in Order.Rooms)
            {
                r.Type = r.GetType();
            }
        }
    }
}
