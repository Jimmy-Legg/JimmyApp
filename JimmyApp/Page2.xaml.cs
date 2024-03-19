using Newtonsoft.Json;
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
            BindingContext = new MainViewModel();
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

    public class Beer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string? _name;
        private string? _price;
        private string? _image;
        private int _id;
        private double _average;
        private int _reviews;

        public string? Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string? Price { get => _price; set { _price = value; OnPropertyChanged(nameof(Price)); } }
        public string? Image { get => _image; set { _image = value; OnPropertyChanged(nameof(Image)); } }
        public int Id { get => _id; set { _id = value; OnPropertyChanged(nameof(Id)); } }
        public double Average { get => _average; set { _average = value; OnPropertyChanged(nameof(Average)); } }
        public int Reviews { get => _reviews; set { _reviews = value; OnPropertyChanged(nameof(Reviews)); } }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MainViewModel : INotifyPropertyChanged
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

        public MainViewModel()
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
                IsLoaded= false;
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
