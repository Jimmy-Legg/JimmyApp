using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using JimmyApp.Services;

namespace JimmyApp.ViewModels
{
    public class Page3ViewModel : BaseViewModel
    {
        private string _beerName;
        private string _price;
        private string _imagePath;
        private readonly MainViewModelPage2 _mainViewModel;
        private readonly IMessageService _messageService;

        public string BeerName
        {
            get => _beerName;
            set
            {
                _beerName = value;
                OnPropertyChanged(nameof(BeerName));
            }
        }

        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public ICommand PickImageCommand { get; }
        public ICommand AddBeerCommand { get; }

        public Page3ViewModel()
        {
            _mainViewModel = ((AppShell)Application.Current.MainPage).BindingContext as MainViewModelPage2;
            _messageService = new MessageService();

            PickImageCommand = new Command(async () => await PickImageAsync());
            AddBeerCommand = new Command(AddBeer);
        }

        private async Task PickImageAsync()
        {
            PickOptions options = new PickOptions
            {
                PickerTitle = "Select an image",
                FileTypes = FilePickerFileType.Images
            };

            FileResult result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                ImagePath = result.FullPath;
            }
        }

        private void AddBeer()
        {
            if (string.IsNullOrWhiteSpace(BeerName))
            {
                _messageService.DisplayAlertAsync("Error", "Le nom est requis", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Price))
            {
                _messageService.DisplayAlertAsync("Error", "Le prix est requis", "OK");
                return;
            }

            if (!double.TryParse(Price, out double priceValue))
            {
                _messageService.DisplayAlertAsync("Error", "Le prix doit être un nombre valide", "OK");
                return;
            }

            int id = _mainViewModel.Beers.Count + 1;
            string image = ImagePath ?? "defaultbeerimage.png";
            double average = 0;
            int reviews = 0;

            var newBeer = new Beer
            {
                Id = id,
                Name = BeerName,
                Price = "$ " + priceValue.ToString(),
                Image = image,
                Average = average,
                Reviews = reviews
            };

            _mainViewModel.AddBeer(id, BeerName, priceValue.ToString(), image, average, reviews);

            var appShell = (AppShell)Application.Current.MainPage;
            appShell.NewBeer = newBeer;

            BeerName = "";
            Price = "$";
            ImagePath = "";
        }
    }
}
