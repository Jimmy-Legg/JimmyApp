namespace JimmyApp
{
    public partial class Page4 : ContentPage
    {
        int count = 0;

        public Page4()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
