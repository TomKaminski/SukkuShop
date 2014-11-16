using System.Collections.Generic;
using System.Linq;

namespace SukkuShop.Models
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public void AddItem(int id, int quantity = 1)
        {
            var line = _lineCollection.FirstOrDefault(p => p.Id == id);

            if (line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Id = id,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void Clear()
        {
            _lineCollection.Clear();
        }

        public void RemoveLine(int id)
        {
            _lineCollection.RemoveAll(l => l.Id == id);
        }

        public IEnumerable<CartLine> Lines
        {
            get { return _lineCollection; }
        }


        public class CartLine
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
        }
    }
}