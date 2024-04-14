using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace JimmyApp.Services
{
    public class MessageService : IMessageService
    {
        public async Task DisplayAlertAsync(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
