namespace JimmyApp
{
    public partial class Page1 : ContentPage
    {
        public Page1(CaroucelValues caroucelValues)
        {
            InitializeComponent();
            BindingContext = caroucelValues;
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageGif());
        }
    }
}
