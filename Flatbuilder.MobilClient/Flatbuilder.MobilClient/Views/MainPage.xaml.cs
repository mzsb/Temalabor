﻿using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fb.MC.Views
{
	public partial class MainPage : ContentPage
	{
        public string UserName { get; }

        public MainPage()
        {
            InitializeComponent();
        }
		public MainPage (string username)
		{
			InitializeComponent ();

		}
	}
}