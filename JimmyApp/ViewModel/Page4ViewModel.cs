using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JimmyApp.ViewModels;

namespace JimmyApp
{
    public class Page4ViewModel : BaseViewModel
    {
        private const string ApiUrl = "https://api.sampleapis.com/beers/ale";
        private readonly HttpClient _httpClient;
        private readonly Random _random;
        private Beer _selectedBeer;

        public Page4ViewModel()
        {
            _httpClient = new HttpClient();
            _random = new Random();
        }

        public Beer SelectedBeer
        {
            get => _selectedBeer;
            set
            {
                _selectedBeer = value;
                OnPropertyChanged(nameof(SelectedBeer));
            }
        }

        public async Task<Beer> GenerateRandomBeerAsync()
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
                    selectedBeer.Image = selectedBeer.Image;
                }
                else
                {
                    selectedBeer.Image = "defaultbeerimage.png";
                }

                SelectedBeer = selectedBeer;

                return selectedBeer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching random beer: {ex.Message}");
                return null;
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
