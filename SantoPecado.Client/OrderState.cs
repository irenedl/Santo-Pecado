using System.Collections.Generic;

namespace SantoPecado.Client
{
    public class OrderState
    {
        public bool ShowingConfigureDialog { get; private set; }

        public Burger ConfiguringBurger { get; private set; }

        public Order Order { get; private set; } = new Order();

        public void ShowConfigureBurgerDialog(BurgerSpecial special)
        {
            ConfiguringBurger = new Burger()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Burger.DefaultSize,
                Toppings = new List<BurgerTopping>(),
            };

            ShowingConfigureDialog = true;
        }

        public void CancelConfigureBurgerDialog()
        {
            ConfiguringBurger = null;
            ShowingConfigureDialog = false;
        }

        public void ConfirmConfigureBurgerDialog()
        {
            Order.Burgers.Add(ConfiguringBurger);
            ConfiguringBurger = null;

            ShowingConfigureDialog = false;
        }

        public void RemoveConfiguredBurger(Burger burger)
        {
            Order.Burgers.Remove(burger);
        }

        public void ResetOrder()
        {
            Order = new Order();
        }
    }
}
