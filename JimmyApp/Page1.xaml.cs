namespace JimmyApp
{
    public partial class Page1 : ContentPage
    {
        public Page1(ViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageGif());
        }
    }
}
