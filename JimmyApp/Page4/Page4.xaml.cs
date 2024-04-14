using Microsoft.Maui.Controls;
using JimmyApp.ViewModels;
using System;

namespace JimmyApp
{
    public partial class Page4 : ContentPage
    {
        private Page4ViewModel _viewModel;
        private MainViewModelPage2 _mainViewModel;

        public Page4()
        {
            InitializeComponent();
            _viewModel = new Page4ViewModel();
            _mainViewModel = ((AppShell)Application.Current.MainPage).BindingContext as MainViewModelPage2;
            BindingContext = _viewModel;
        }

        private async void GenerateBeer_Clicked(object sender, EventArgs e)
        {
            var selectedBeer = await _viewModel.GenerateRandomBeerAsync();

            if (selectedBeer != null)
            {
                beerName.Text = $"Nom: {selectedBeer.Name}";
                beerPrice.Text = $"Prix: {selectedBeer.Price}";
                beerImage.Source = selectedBeer.Image;
            }
        }

        private void AddToFavorites_Clicked(object sender, EventArgs e)
        {
            if (_viewModel.SelectedBeer != null)
            {
                _mainViewModel.AddBeer(
                    _viewModel.SelectedBeer.Id,
                    _viewModel.SelectedBeer.Name,
                    _viewModel.SelectedBeer.Price,
                    _viewModel.SelectedBeer.Image,
                    _viewModel.SelectedBeer.Average,
                    _viewModel.SelectedBeer.Reviews);

                DisplayAlert("Success", "Beer added to favorites!", "OK");
            }
            else
            {
                DisplayAlert("Error", "No beer selected!", "OK");
            }
        }
    }
}
