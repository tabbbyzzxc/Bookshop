﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bookshop.Repositories;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for MissingPage.xaml
    /// </summary>
    public partial class MissingPage : Page
    {
        public MissingPage()
        {
            BookRepository repo = new BookRepository();
            
            InitializeComponent();
            listView.ItemsSource = repo.GetMissingBooks();
        }
    }
}
