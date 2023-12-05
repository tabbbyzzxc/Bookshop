using Bookshop.ProductsLib;
using DataAccess;
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
            var filteredInvoices = db.Invoices
                .Where(x => x.Date >= fromDate && x.Date <= toDate && x.InvoiceType == type)
                .Include(x => x.InvoiceLines)
                .ToList();
            var productIds = filteredInvoices.SelectMany(x => x.InvoiceLines).Distinct().Select(x => x.ProductUniqueId).ToList();
            var products = productService.GetProductsByIds(productIds);
            foreach (var invoice in filteredInvoices)
            {
                foreach (var invoiceLine in invoice.InvoiceLines)
                {
                    invoiceLine.Product = products.FirstOrDefault(x => x.UniqueId == invoiceLine.ProductUniqueId);
                }
            }

            return filteredInvoices;
        }
    }
}
