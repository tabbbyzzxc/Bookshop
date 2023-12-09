using Bookshop.ProductsLib;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.Windows.Media;

namespace Bookshop.Services
{
    public class DocumentService
    {
        public FixedDocumentSequence CreatePrintDocument(Order order)
        {
            var flowDoc = new FlowDocument();
            
            var headerPara = new Paragraph
            {
                TextAlignment = TextAlignment.Center
            };
            var headerText = new Run
            {
                Text = order.Name,
                FontSize = 20
            };
            headerPara.Inlines.Add(headerText);
            flowDoc.Blocks.Add(headerPara);

            var datePara = new Paragraph
            {
                TextAlignment = TextAlignment.Right
            };
            var dateText = new Run
            {
                Text = order.Date.ToString("dd.MM.yyyy"),
                FontSize = 16
            };
            datePara.Inlines.Add(dateText);
            flowDoc.Blocks.Add(datePara);

            var table1 = new Table
            {
                CellSpacing = 10
            };
            flowDoc.Blocks.Add(table1);
            int numberOfColumns = 5;
            for (int i = 0; i < numberOfColumns; i++)
            {
                table1.Columns.Add(new TableColumn());
            }

            // Create and add an empty TableRowGroup to hold the table's Rows.
            table1.RowGroups.Add(new TableRowGroup());
            table1.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table1.RowGroups[0].Rows[0];

            // Global formatting for the header row.
            currentRow.FontSize = 12;
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Background = Brushes.Beige;

            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Code"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Product"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Price"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Quantity"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Total"))));

            for (int i = 1; i <= order.OrderList.Count; i++)
            {
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[i];
                var orderLine = order.OrderList[i - 1];

                // Global formatting for the row.
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;

                // Add cells with content to the third row.
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(orderLine.Book.ProductCode))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(orderLine.Book.MainData))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(orderLine.Book.SellPrice.ToString("0.00")))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(orderLine.Quantity.ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(orderLine.Total.ToString("0.00")))));
            }

            var orderTotalPara = new Paragraph
            {
                TextAlignment = TextAlignment.Left
            };
            var orderTotalText = new Run
            {
                Text = $"Total: {order.Total.ToString("0.00")}",
                FontSize = 18
            };
            orderTotalPara.Inlines.Add(orderTotalText);
            flowDoc.Blocks.Add(orderTotalPara);

            if (File.Exists("printPreview.xps"))
            {
                File.Delete("printPreview.xps");
            }
            using var xpsDocument = new XpsDocument("printPreview.xps", FileAccess.ReadWrite);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(((IDocumentPaginatorSource)flowDoc).DocumentPaginator);

            return xpsDocument.GetFixedDocumentSequence();
        }

        public FixedDocumentSequence CreatePrintDocument(Invoice invoice)
        {
            var flowDoc = new FlowDocument();

            var headerPara = new Paragraph
            {
                TextAlignment = TextAlignment.Center
            };
            var headerText = new Run
            {
                Text = invoice.Name,
                FontSize = 20
            };
            headerPara.Inlines.Add(headerText);
            flowDoc.Blocks.Add(headerPara);

            var datePara = new Paragraph
            {
                TextAlignment = TextAlignment.Right
            };
            var dateText = new Run
            {
                Text = invoice.Date.ToString("dd.MM.yyyy"),
                FontSize = 16
            };
            datePara.Inlines.Add(dateText);
            flowDoc.Blocks.Add(datePara);

            var table1 = new Table
            {
                CellSpacing = 10
            };
            flowDoc.Blocks.Add(table1);
            int numberOfColumns = 5;
            for (int i = 0; i < numberOfColumns; i++)
            {
                table1.Columns.Add(new TableColumn());
            }

            // Create and add an empty TableRowGroup to hold the table's Rows.
            table1.RowGroups.Add(new TableRowGroup());
            table1.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table1.RowGroups[0].Rows[0];

            // Global formatting for the header row.
            currentRow.FontSize = 12;
            currentRow.FontWeight = FontWeights.Bold;
            currentRow.Background = Brushes.Beige;

            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Code"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Product"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Price"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Quantity"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Total"))));

            for (int i = 1; i <= invoice.InvoiceLines.Count; i++)
            {
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[i];
                var orderLine = invoice.InvoiceLines[i - 1];

                // Global formatting for the row.
                currentRow.FontSize = 12;
                currentRow.FontWeight = FontWeights.Normal;

                // Add cells with content to the third row.
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(orderLine.Product.ProductCode))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(orderLine.Product.MainData))));
                var price = invoice.InvoiceType == InvoiceType.Income ? orderLine.Product.BuyPrice.ToString("0.00") : orderLine.Product.SellPrice.ToString("0.00");
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(price))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(orderLine.Quantity.ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(orderLine.Total.ToString("0.00")))));
            }

            var orderTotalPara = new Paragraph
            {
                TextAlignment = TextAlignment.Left
            };
            var orderTotalText = new Run
            {
                Text = $"Total: {invoice.Total.ToString("0.00")}",
                FontSize = 18
            };
            orderTotalPara.Inlines.Add(orderTotalText);
            flowDoc.Blocks.Add(orderTotalPara);

            if (File.Exists("printPreview.xps"))
            {
                File.Delete("printPreview.xps");
            }
            using var xpsDocument = new XpsDocument("printPreview.xps", FileAccess.ReadWrite);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(((IDocumentPaginatorSource)flowDoc).DocumentPaginator);

            return xpsDocument.GetFixedDocumentSequence();
        }
    }
}
