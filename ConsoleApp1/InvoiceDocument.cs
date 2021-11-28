using ConsoleApp1.Models;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1 {
  internal class InvoiceDocument : IDocument {

    public InvoiceModel Model { get; }

    public InvoiceDocument(InvoiceModel model) {
      Model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container) {
      container
          .Page(page => {
            page.Margin(50);

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);

            page.Footer().AlignCenter().Text(x =>
            {
              x.CurrentPageNumber();
              x.Span(" / ");
              x.TotalPages();
            });
          });
    }

    private void ComposeHeader(IContainer container) {
      TextStyle titleStyle = 
        TextStyle.Default.Size(20).SemiBold().Color(Colors.Blue.Medium);

      container.Row(row =>
      {
        row.RelativeColumn().Stack(stack =>
        {
          stack.Item().Text($"Invoice #{Model.InvoiceNumber}", titleStyle);

          stack.Item().Text(text =>
          {
            text.Span("Issue date: ", TextStyle.Default.SemiBold());
            text.Span($"{Model.IssueDate:d}");
          });

          stack.Item().Text(text =>
          {
            text.Span("Due date: ", TextStyle.Default.SemiBold());
            text.Span($"{Model.DueDate:d}");
          });
        });

        row.ConstantColumn(100).Height(50).Placeholder();
      });
    }

    private void ComposeContent(IContainer container) {
      container.PaddingVertical(40).Stack(stack =>
      {
        stack.Spacing(5);

        stack.Item().Row(row =>
        {
          row.RelativeColumn().Component(new AddressComponent("From", Model.SellerAddress));
          row.ConstantColumn(50);
          row.RelativeColumn().Component(new AddressComponent("For", Model.CustomerAddress));
        });

        stack.Item().Element(ComposeTable);

        if (!string.IsNullOrWhiteSpace(Model.Comments))
          stack.Item().PaddingTop(25).Element(ComposeComments);
      });
    }

    private void ComposeTable(IContainer container) {
      container.PaddingTop(10).Decoration(decoration =>
      {
        // header
        decoration.Header().BorderBottom(1).Padding(5).Row(row =>
        {
          row.ConstantColumn(25).Text("#");
          row.RelativeColumn(3).Text("Product");
          row.RelativeColumn().AlignRight().Text("Unit price");
          row.RelativeColumn().AlignRight().Text("Quantity");
          row.RelativeColumn().AlignRight().Text("Total");
        });

        // content
        decoration
            .Content()
            .Stack(stack => {
              foreach (var item in Model.Items) {
                stack.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Row(row =>
                {
                  row.ConstantColumn(25).Text(Model.Items.IndexOf(item) + 1);
                  row.RelativeColumn(3).Text(item.Name);
                  row.RelativeColumn().AlignRight().Text($"{item.Price}$");
                  row.RelativeColumn().AlignRight().Text(item.Quantity);
                  row.RelativeColumn().AlignRight().Text($"{item.Price * item.Quantity}$");
                });
              }
            });
      });
    }

    private void ComposeComments(IContainer container) {
      container.Background(Colors.Grey.Lighten3).Padding(10).Stack(stack =>
      {
        stack.Spacing(5);
        stack.Item().Text("Comments", TextStyle.Default.Size(14));
        stack.Item().Text(Model.Comments);
      });
    }
  }
}
