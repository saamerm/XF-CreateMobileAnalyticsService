using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SimpleAnalytics
{
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DataCall("PageView: Home");
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage.DisplayAlert("Alert", "Message Sent", "OK");
            await DataCall("SelectAction: SendMessage");
        }

        async void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            await DataCall("SelectAction: LinkedIn");
            Device.OpenUri(new Uri("https://www.linkedin.com/in/saamer"));
        }

        async Task DataCall(string eventName)
        {
            var client = new HttpClient();
            var model = new RequestModel()
            {
                Event = eventName
            };
            var uri = "https://script.google.com/macros/s/AKfycbwEfzUX-5BUR_-09kzTzEZ4JmmBEdA8GKE25pLIB3OGuEqV6MM/exec";
            var jsonString = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(jsonString);
            var result = await client.PostAsync(uri, requestContent);
            var resultContent = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultContent);
        }
    }
}