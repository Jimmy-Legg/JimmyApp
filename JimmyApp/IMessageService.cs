using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyApp
{
    public interface IMessageService
    {
        Task DisplayAlertAsync(string title, string message, string cancel);
    }

}
