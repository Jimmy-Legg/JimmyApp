using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JimmyApp
{
    public partial class Page3 : ContentPage
    {
        Page3ViewModel viewModel;

        public Page3()
        {
            InitializeComponent();
            viewModel = new Page3ViewModel();
            BindingContext = viewModel;
        }
        private void AddBeer_Clicked(object sender, EventArgs e)
        {
            viewModel.AddBeer(beerNameEntry.Text, priceEntry.Text);
            beerNameEntry.Text = "";
            priceEntry.Text = "";
        }

    }

    public class Page3ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<CreatedBeer> _beerList;

        public ObservableCollection<CreatedBeer> BeerList
        {
            get { return _beerList; }
            set
            {
                _beerList = value;
                OnPropertyChanged(nameof(BeerList));
            }
        }

        public Page3ViewModel()
        {
            BeerList = new ObservableCollection<CreatedBeer>();
        }

        public void AddBeer(string name, string price)
        {
            Random rand = new Random();
            var newBeer = new CreatedBeer
            {
                Id = rand.Next(),
                Name = name,
                Price = price,
                Image = "beer.png",
                Average = 0,
                Reviews = 0 
            };

            // Add the new beer to the top of the list
            BeerList.Insert(0, newBeer);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
