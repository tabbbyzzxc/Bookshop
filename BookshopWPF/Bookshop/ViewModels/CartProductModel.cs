using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bookshop.ViewModels
{
    public class CartProductModel : INotifyPropertyChanged
    {
        private int _quantity;

        public string ProductCode { get; set; }

        public Guid UniqueId { get; set; }

        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public decimal Price { get; set; }

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                OnProperyChanged("Total");
            }
        }

        public decimal Total => Quantity * Price;
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnProperyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

    }
}
