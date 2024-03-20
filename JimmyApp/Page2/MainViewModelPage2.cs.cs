using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JimmyApp
{
    public class MainViewModelPage2 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Beer> _beers;
        private List<Beer> _renderingBeers;
        private int _itemsToAdd = 10;
        private bool _isLoading;
        private bool _isLoaded;
        private Beer _selectedBeer;

        public List<Beer> Beers { get => _beers; set { _beers = value; OnPropertyChanged(nameof(Beers)); } }
        public List<Beer> RenderingBeers { get => _renderingBeers; set { _renderingBeers = value; OnPropertyChanged(nameof(RenderingBeers)); } }
        public int ItemsToAdd { get => _itemsToAdd; set { _itemsToAdd = value; OnPropertyChanged(nameof(ItemsToAdd)); } }
        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); } }
        public bool IsLoaded { get => _isLoaded; set { _isLoaded = value; OnPropertyChanged(nameof(IsLoaded)); } }

        public Beer SelectedBeer { get => _selectedBeer; set { _selectedBeer = value; OnPropertyChanged(nameof(SelectedBeer)); OnPropertyChanged(nameof(IsBeerSelected)); } }

        public bool IsBeerSelected => SelectedBeer != null;
        public ICommand LoadMoreCommand { get; }

        public MainViewModelPage2()
        {
            // Initialize the command
            LoadMoreCommand = new Command(ExecuteLoadMoreCommand);

            RenderingBeers = new List<Beer>();
            Task.Run(async () => await InitializeDataAsync());
        }

        // Command execution logic
        private async void ExecuteLoadMoreCommand()
        {
            // Increase the number of items to add by 10
            ItemsToAdd += 10;

            // Load more data
            await InitializeDataAsync();
        }

        private async Task InitializeDataAsync()
        {
            try
            {
                IsLoaded = false;
                IsLoading = true;

                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync("https://api.sampleapis.com/beers/ale");
                    var beerData = JsonConvert.DeserializeObject<List<Beer>>(response).GetRange(0, ItemsToAdd);

                    await FetchBeerDetailsAsync(client, beerData);

                    Beers = beerData;
                    RenderingBeers = Beers;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                IsLoaded = true;
                IsLoading = false;
            }
        }

        private async Task FetchBeerDetailsAsync(HttpClient client, List<Beer> beers)
        {
            try
            {
                foreach (var beer in beers)
                {
                    var detailResponse = await client.GetStringAsync($"https://api.sampleapis.com/beers/ale/{beer.Id}");
                    var detailData = JsonConvert.DeserializeAnonymousType(detailResponse, new { price = "", rating = new { average = 0.0, reviews = 0 }, image = "" });

                    beer.Price = detailData.price;
                    beer.Average = detailData.rating.average;
                    beer.Reviews = detailData.rating.reviews;
                    beer.Image = detailData.image;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions for individual beer detail fetch
                Console.WriteLine($"Error fetching details: {ex.Message}");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
