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
                Title = "Title 1",
                Description = "Description 1",
                Image = "image1.png"
            });
            CarouselItems.Add(new CarouselItem
            {
                Title = "Title 2",
                Description = "Description 2",
                Image = "image2.png"
            });
            CarouselItems.Add(new CarouselItem
            {
                Title = "Title 3",
                Description = "Description 3",
                Image = "image3.png"
            });
        }
    }
}
