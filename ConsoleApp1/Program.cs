using ConsoleApp1.Models;
using QuestPDF.Fluent;
using System.Diagnostics;

namespace ConsoleApp1 {

  /// <summary>
  /// https://www.questpdf.com/documentation/getting-started.html#getting-started
  /// </summary>
  class Program {
    static void Main(string[] args) {

      var filePath = "invoice.pdf";

      InvoiceModel model = InvoiceDocumentDataSource.GetInvoiceDetails();
      InvoiceDocument document = new InvoiceDocument(model);
      document.GeneratePdf(filePath);

      Process.Start("explorer.exe", filePath);
    }
  }
}
