using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Flatbuilder.DTO;
using FreshMvvm;
using Xamarin.Forms;

namespace Fb.MC.Views
{
    class NewOrderPageModel : FreshBasePageModel
    {
        public ICommand CreateOrderCommand { get; }
        private DateTime startDate;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                RaisePropertyChanged("StartDate");
            }
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }
        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                RaisePropertyChanged("Price");
            }
        }
        private List<Room> rooms;

        public NewOrderPageModel()
        {
            CreateOrderCommand = new Command(
                execute: async () =>
                {

                }
                );
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(5);
           // PropertyChanged += OnPropertyChanged;
        }
        protected void OnPropertyChanged(object sender, EventArgs args)
        {
           double pr=0;
           double days = (EndDate - StartDate).TotalDays;

            foreach (Room r in rooms)
            {
                pr += r.Price * days;
            }
            Price = pr;
        }
    }
}
