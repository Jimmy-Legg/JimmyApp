using Microsoft.Maui.Controls;
using JimmyApp.ViewModels;

namespace JimmyApp
{
    public partial class Page3 : ContentPage
    {
        private Page3ViewModel _viewModel;

        public Page3()
        {
            InitializeComponent();
            _viewModel = new Page3ViewModel();
            BindingContext = _viewModel;
        }
    }
}
