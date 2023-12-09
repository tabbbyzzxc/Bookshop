﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshop.ProductsLib
{
    public class OrderLine
    {
        public long Id { get; set; }

        public Guid ProductUniqueId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; }

        public long OrderId { get; set; }

        /*[NotMapped]*/
        public Book Book { get; set; }

        public decimal Total => Quantity * Book.SellPrice;

    }
}
