﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.ViewModels
{
    public class OrderLineViewModel : INotifyPropertyChanged
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
                _quantity = value;
                OnProperyChanged("Total");
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


    }
}