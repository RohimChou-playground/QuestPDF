using ConsoleApp1.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1 {
  internal class AddressComponent : IComponent {
    
    private string Title { get; }
    private Address Address { get; }

    public AddressComponent(string title, Address address) {
      Title = title;
      Address = address;
    }

    public void Compose(IContainer container) {
      container.Stack(stack =>
      {
        stack.Spacing(2);

        stack.Item().BorderBottom(1).PaddingBottom(5).Text(Title, TextStyle.Default.SemiBold());

        stack.Item().Text(Address.CompanyName);
        stack.Item().Text(Address.Street);
        stack.Item().Text($"{Address.City}, {Address.State}");
        stack.Item().Text(Address.Email);
        stack.Item().Text(Address.Phone);
      });
    }
  }
}
