using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyApp
{

    public partial class CaroucelValues : ObservableObject
    {
        public ObservableCollection<CarouselItem> CarouselItems { get; set; } = new ();
        public CaroucelValues()
        {
            CarouselItems.Add(new CarouselItem
            {
                Title = "Les Bières",
                Description = "Elles ont différentes couleurs ! ",
                Image = "image1.png"
            });
            CarouselItems.Add(new CarouselItem
            {
                Title = "Profit",
                Description = "Comment gagner de l'argent",
                Image = "image2.png"
            });
            CarouselItems.Add(new CarouselItem
            {
                Title = "Le partage",
                Description = "N'hésitez pas !",
                Image = "image3.png"
            });
        }
    }
}
