﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace JimmyApp
{
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
            BindingContext = new MainViewModelPage2();
        }
        private async void ViewCell_Tapped(object sender, EventArgs e)
        {
            var viewCell = (ViewCell)sender;
            var beer = (Beer)viewCell.BindingContext;

            await Navigation.PushAsync(new BeerDetailsPage
            {
                Beer = beer,
                BindingContext = beer
            });
        }

    }

    
}