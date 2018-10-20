using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Flatbuilder.MobilClient.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Str { get; set; }
    }
}
