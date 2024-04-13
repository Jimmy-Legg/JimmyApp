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
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(100);
            gif.IsAnimationPlaying = false;
            await Task.Delay(100);
            gif.IsAnimationPlaying = true;
        }
    }
}