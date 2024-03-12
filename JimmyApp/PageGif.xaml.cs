namespace JimmyApp
{
    public partial class PageGif : ContentPage
    {
        public PageGif()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}