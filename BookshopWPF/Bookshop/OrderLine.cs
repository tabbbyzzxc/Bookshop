using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    public class OrderLine : INotifyPropertyChanged
    {
        private int _quantity;
        public long Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal SellPrice { get; set; }

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (value <= FindMaxQuantity(Id))
                {
                    _quantity = value;
                    OnProperyChanged("Total");
                }
                else
                {
                    _quantity = FindMaxQuantity(Id);
                    OnProperyChanged("Total");
                    
                }
            }
        }

        public decimal Total
        {
            get
            {
                return SellPrice * Quantity;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnProperyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private int FindMaxQuantity(long id)
        {
            var repo = new BookQuantityRepository();
            var bookList = repo.GetBooksIds();
            foreach (var item in bookList)
            {
                if (item.BookId == id)
                {
                    return item.Quantity;
                }
            }
            return 0;
        }
    }
}
