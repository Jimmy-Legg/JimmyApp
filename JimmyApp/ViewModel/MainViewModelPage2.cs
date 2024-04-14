using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Generic;

namespace JimmyApp
{
    public class MainViewModelPage2 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Beer> _beers;
        private bool _isLoading;
        private bool _isLoaded;
        private Beer _selectedBeer;

        public ObservableCollection<Beer> Beers
        {
            get => _beers;
            set
            {
                _beers = value;
                OnPropertyChanged(nameof(Beers));
            }
        }
        
        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); } }
        public bool IsLoaded { get => _isLoaded; set { _isLoaded = value; OnPropertyChanged(nameof(IsLoaded)); } }

        public Beer SelectedBeer
        {
            get => _selectedBeer;
            set
            {
                _selectedBeer = value;
                OnPropertyChanged(nameof(SelectedBeer));
                OnPropertyChanged(nameof(IsBeerSelected));
            }
        }

        public bool IsBeerSelected => SelectedBeer != null;

        public MainViewModelPage2()
        {
            Beers = new ObservableCollection<Beer>();
            Task.Run(async () => await InitializeDataAsync());
            Console.WriteLine(Beers);
        }

        public void AddBeer(int id, string name, string price, string image, double average, int reviews)
        {
            var newBeer = new Beer { Id = id, Name = name, Price = price, Image = image, Average = average, Reviews = reviews };
            Beers.Add(newBeer);
            OnPropertyChanged(nameof(Beers));
        }

        public void PrintBeers()
        {
            foreach (var beer in Beers)
            {
                Console.WriteLine($"ID: {beer.Id}, Name: {beer.Name}, Price: {beer.Price}, Image: {beer.Image}, Average: {beer.Average}, Reviews: {beer.Reviews}");
            }
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
                    var beerData = JsonConvert.DeserializeObject<List<Beer>>(response).GetRange(0, 10);

                    await FetchBeerDetailsAsync(client, beerData);

                    foreach (var beer in beerData)
                    {
                        if (string.IsNullOrEmpty(beer.Image))
                        {
                            beer.Image = "defaultbeerimage.png";
                        }
                    }

                    Beers = new ObservableCollection<Beer>(beerData);
                }
            }
            catch (Exception ex)
            {
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

                    if (string.IsNullOrEmpty(beer.Image))
                    {
                        beer.Image = "defaultbeerimage.png";
                    }
                    else
                    {
                        var imageResponse = await client.GetAsync(beer.Image);
                        if (!imageResponse.IsSuccessStatusCode)
                        {
                            beer.Image = "defaultbeerimage.png";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching details: {ex.Message}");
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
