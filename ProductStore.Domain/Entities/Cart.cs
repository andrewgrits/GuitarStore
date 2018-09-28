using System.Collections.Generic;
using System.Linq;

namespace ProductStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Guitar guitar, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Guitar.Id == guitar.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Guitar = guitar,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Guitar guitar)
        {
            lineCollection.RemoveAll(l => l.Guitar.Id == guitar.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Guitar.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Guitar Guitar { get; set; }
        public int Quantity { get; set; }
    }
}
