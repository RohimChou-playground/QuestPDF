using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2 {

  /// <summary>
  /// My Hello World
  /// </summary>
  internal class Program {
    static void Main(string[] args) {

      new MyPdfDoc().GeneratePdf("aaa.pdf");

      Process.Start("explorer.exe", "aaa.pdf");
    }
  }

  internal class MyPdfDoc : IDocument {
    public DocumentMetadata GetMetadata() {
      return DocumentMetadata.Default;
    }

    public void Compose(IDocumentContainer container) {
      container.Page(page => {
        page.MarginVertical(20);
        page.MarginHorizontal(10);

        page.Content().Background(Colors.Grey.Lighten3).Row(row => {
          row.RelativeColumn().Stack(stack => {
            stack.Item().Text(
              "Title1",
              TextStyle.Default.Size(20).Bold().Color(Colors.Blue.Darken1));

            stack.Item().Text("line1 with some text.");
            stack.Item().Text("line2 with some text.");
            stack.Item().PaddingTop(10).Text("line3 with some text.");
          });
        });
      });
    }
  }
}
