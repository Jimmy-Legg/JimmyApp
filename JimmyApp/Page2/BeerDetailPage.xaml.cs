

namespace JimmyApp;
public partial class BeerDetailsPage : ContentPage
{
    public BeerDetailsPage()
    {
        InitializeComponent();
    }

    public Beer Beer { get; set; }

    private async void Modify_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ModifyBeerPage { BindingContext = this.BindingContext });
    }


}
