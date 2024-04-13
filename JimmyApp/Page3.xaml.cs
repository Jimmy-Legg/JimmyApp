using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Maui.Storage;


namespace JimmyApp
{
    public partial class Page3 : ContentPage
    {
        MainViewModelPage2 mainViewModel;
        public event EventHandler<Beer> BeerAdded;
        private string _imagePath;

        public Page3()
        {
            InitializeComponent();
            mainViewModel = ((AppShell)Application.Current.MainPage).BindingContext as MainViewModelPage2;
            BindingContext = mainViewModel;
        }

        private async void PickImage_Clicked(object sender, EventArgs e)
        {
            PickOptions options = new PickOptions
            {
                PickerTitle = "Select an image",
                FileTypes = FilePickerFileType.Images
            };

            FileResult result = await FilePicker.Default.PickAsync(options);
            Console.WriteLine("Result: " + result);
            if (result != null)
            {
                _imagePath = result.FullPath;
                imagePathLabel.Text = $"Selected image path: {_imagePath}";

            }
        }



        private void AddBeer_Clicked(object sender, EventArgs e)
        {
            string name = beerNameEntry.Text;
            string price = priceEntry.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                DisplayAlert("Error", "Beer name is required", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(price))
            {
                DisplayAlert("Error", "Price is required", "OK");
                return;
            }

            if (!double.TryParse(price, out double priceValue))
            {
                DisplayAlert("Error", "Price should be a valid number", "OK");
                return;
            }

            int id = mainViewModel.Beers.Count + 1;
            string image = _imagePath;
            double average = 0;
            int reviews = 0;

            // Set default beer image if the image path is empty
            if (string.IsNullOrEmpty(image))
            {
                image = "defaultbeerimage.png";
            }

            var newBeer = new Beer { Id = id, Name = name, Price = "$" + priceValue.ToString(), Image = image, Average = average, Reviews = reviews };
            mainViewModel.AddBeer(id, name, priceValue.ToString(), image, average, reviews);

            var appShell = (AppShell)Application.Current.MainPage;
            appShell.NewBeer = newBeer;

            beerNameEntry.Text = "";
            priceEntry.Text = "";
            _imagePath = null;

            imagePathLabel.Text = "";
        }


    }

}
