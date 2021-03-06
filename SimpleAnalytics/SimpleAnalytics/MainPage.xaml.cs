﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace SimpleAnalytics
{
    public partial class MainPage : ContentPage
    {
        // SelectAction
        // PageView

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DataCall("PageView: Home");

        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage.DisplayAlert("", "Message Sent", "OK");
            await DataCall("SelectAction: SendMessage");
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.linkedin.com/in/saamer"));
            await DataCall("SelectAction: LinkedIn");
        }

        async Task DataCall(string eventName)
        {
            var client = new HttpClient();
            var model = new RequestModel()
            {
                Event = eventName
            };
            var uri = "https://script.google.com/macros/s/AKfycbzK7MhEWrO5fUM4U30Jl8JnFMSj-vYa1g4-2AVNd2Z-JY28x_rF/exec";
            var jsonString = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(jsonString);
            var result = await client.PostAsync(uri, requestContent);

            var resultContent = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultContent);
        }
    }
}
