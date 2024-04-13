namespace JimmyApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new MainViewModelPage2();

        }
        public Beer NewBeer { get; set; }
        
    }
}
