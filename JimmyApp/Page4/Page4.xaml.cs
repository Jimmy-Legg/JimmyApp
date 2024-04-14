using System;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;

namespace JimmyApp
{
    public partial class Page4 : ContentPage
    {
        private const string ApiUrl = "https://api.sampleapis.com/beers/ale";
        private readonly HttpClient _httpClient;
        private readonly Random _random;

        public Page4()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _random = new Random();
        }

        private async void GenerateBeer_Clicked(object sender, EventArgs e)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(ApiUrl);
                var beers = JsonConvert.DeserializeObject<List<Beer>>(response);

                int randomIndex = _random.Next(beers.Count);

                var selectedBeer = beers[randomIndex];

                bool isImageAccessible = await IsImageAccessible(selectedBeer.Image);

                if (isImageAccessible)
                {
                     beerImage.Source = selectedBeer.Image;
                }
                else
                {
                    beerImage.Source = "defaultbeerimage.png";
                }

                beerName.Text = $"Nom: {selectedBeer.Name}";
                beerPrice.Text = $"Prix: {selectedBeer.Price}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching random beer: {ex.Message}");
            }
        }

        private async Task<bool> IsImageAccessible(string imageUrl)
        {
            try
            {
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, imageUrl));

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }




    }
}
