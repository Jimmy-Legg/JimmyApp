using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyApp
{

    public partial class ViewModel : ObservableObject
    {
        public ObservableCollection<CarouselItem> CarouselItems { get; set; } = new ();
        public ViewModel()
        {
            CarouselItems.Add(new CarouselItem
            {
                Title = "Banks",
                Description = "Here you can see a bank !",
                Image = "image1.png"
            });
            CarouselItems.Add(new CarouselItem
            {
                Title = "Profit",
                Description = "How can you earn money ?",
                Image = "image2.png"
            });
            CarouselItems.Add(new CarouselItem
            {
                Title = "Cash",
                Description = "Cash is good !",
                Image = "image3.png"
            });
        }
    }
}
