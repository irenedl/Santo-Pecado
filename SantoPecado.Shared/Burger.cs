using System.Collections.Generic;
using System.Linq;

namespace SantoPecado
{
    /// <summary>
    /// Represents a customized burger as part of an order
    /// </summary>
    public class Burger
    {
        public const int DefaultSize = 12;
        public const int MinimumSize = 8;
        public const int MaximumSize = 15;

        public int Id { get; set; }

        public int OrderId { get; set; }

        public BurgerSpecial Special { get; set; }

        public int SpecialId { get; set; }

        public int Size { get; set; }

        public List<BurgerTopping> Toppings { get; set; }

        public decimal GetBasePrice()
        {
            return ((decimal)Size / (decimal)DefaultSize) * Special.BasePrice;
        }

        public decimal GetTotalPrice()
        {
            return GetBasePrice() + Toppings.Sum(t => t.Topping.Price);
        }

        public string GetFormattedTotalPrice()
        {
            return GetTotalPrice().ToString("0.00");
        }
    }
}
