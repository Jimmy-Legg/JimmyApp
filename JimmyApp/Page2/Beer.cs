using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyApp
{
    public class Beer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;
        private string? _name;
        private string? _price;
        private string? _image;
        private double _average;
        private int _reviews;

        public int Id { get => _id; set { _id = value; OnPropertyChanged(nameof(Id)); } }
        public string? Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string? Price { get => _price; set { _price = value; OnPropertyChanged(nameof(Price)); } }
        public string? Image { get => _image; set { _image = value; OnPropertyChanged(nameof(Image)); } }
        public double Average { get => _average; set { _average = value; OnPropertyChanged(nameof(Average)); } }
        public int Reviews { get => _reviews; set { _reviews = value; OnPropertyChanged(nameof(Reviews)); } }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
