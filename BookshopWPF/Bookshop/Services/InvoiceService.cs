using Bookshop.ProductsLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookshop.Services
{
    public class InvoiceService
    {
        private ProductService productService = new ProductService();

        public bool AddInvoice(Invoice invoice)
        {
            var db = new ProductDbContext();
            db.Invoices.Add(invoice);
            db.SaveChanges();
            productService.UpdateQuantities(invoice);

            return true;
        }

        public List<Invoice> GetAllInvoices()
        {
            var db = new ProductDbContext();
            return db.Invoices.Include(x => x.InvoiceLines).ToList();
        }

        public List<Invoice> GetInvoicesByDate(DateTime fromDate, DateTime toDate, InvoiceType type)
        {
            var db = new ProductDbContext();
            var filteredInvoices = db.Invoices.Where(x => x.Date >= fromDate && x.Date <= toDate && x.InvoiceType == type).ToList();
            return filteredInvoices;
        }


    }
}
