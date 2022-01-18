namespace SantoPecado.Server
{
    public static class SeedData
    {
        public static void Initialize(BurgerStoreContext db)
        {
            var toppings = new Topping[]
            {
                new Topping()
                {
                    Name = "Extra cheese",
                    Price = 2.50m,
                },
                new Topping()
                {
                    Name = "American bacon",
                    Price = 2.99m,
                },
                new Topping()
                {
                    Name = "British bacon",
                    Price = 2.99m,
                },
                new Topping()
                {
                    Name = "Canadian bacon",
                    Price = 2.99m,
                },
                new Topping()
                {
                    Name = "Tea and crumpets",
                    Price = 5.00m
                },
                new Topping()
                {
                    Name = "Fresh-baked scones",
                    Price = 4.50m,
                },
                new Topping()
                {
                    Name = "Bell peppers",
                    Price = 1.00m,
                },
                new Topping()
                {
                    Name = "Onions",
                    Price = 1.00m,
                },
                new Topping()
                {
                    Name = "Mushrooms",
                    Price = 1.00m,
                },
                new Topping()
                {
                    Name = "Pepperoni",
                    Price = 1.00m,
                },
                new Topping()
                {
                    Name = "Duck sausage",
                    Price = 3.20m,
                },
                new Topping()
                {
                    Name = "Venison meatballs",
                    Price = 2.50m,
                },
                new Topping()
                {
                    Name = "Served on a silver platter",
                    Price = 250.99m,
                },
                new Topping()
                {
                    Name = "Lobster on top",
                    Price = 64.50m,
                },
                new Topping()
                {
                    Name = "Sturgeon caviar",
                    Price = 101.75m,
                },
                new Topping()
                {
                    Name = "Artichoke hearts",
                    Price = 3.40m,
                },
                new Topping()
                {
                    Name = "Fresh tomatoes",
                    Price = 1.50m,
                },
                new Topping()
                {
                    Name = "Basil",
                    Price = 1.50m,
                },
                new Topping()
                {
                    Name = "Steak (medium-rare)",
                    Price = 8.50m,
                },
                new Topping()
                {
                    Name = "Blazing hot peppers",
                    Price = 4.20m,
                },
                new Topping()
                {
                    Name = "Buffalo chicken",
                    Price = 5.00m,
                },
                new Topping()
                {
                    Name = "Blue cheese",
                    Price = 2.50m,
                },
            };

            var specials = new BurgerSpecial[]
            {
                new BurgerSpecial()
                {
                    Name = "Cheese Burger",
                    Description = "Es una realidad: no hay burger ni más sexy ni más bomba.",
                    BasePrice = 9.99m,
                    ImageUrl = "img/burgers/cheese-burger.png",
                },
                new BurgerSpecial()
                {
                    Id = 2,
                    Name = "Black Burger",
                    Description = "Carne de res con pimienta negra cubierta por salsa Chaliapin y tinta de calamar.",
                    BasePrice = 11.99m,
                    ImageUrl = "img/burgers/black-burger.png",
                },
                new BurgerSpecial()
                {
                    Id = 3,
                    Name = "California Burger",
                    Description = "Si amas el guacamole, deja de buscar, esta es tu burger.",
                    BasePrice = 10.50m,
                    ImageUrl = "img/burgers/california-burger.png",
                },
                new BurgerSpecial()
                {
                    Id = 4,
                    Name = "Eggcelent Burger",
                    Description = "No hay duda: todo con huevo mola más.",
                    BasePrice = 12.75m,
                    ImageUrl = "img/burgers/eggcelent-burger.png",
                },
                new BurgerSpecial()
                {
                    Id = 5,
                    Name = "Red-Beef Burger",
                    Description = "¿Cómo? Carne y remolacha, ¡esta si que empacha!",
                    BasePrice = 11.00m,
                    ImageUrl = "img/burgers/redbeef-burger.png",
                },
                new BurgerSpecial()
                {
                    Id = 6,
                    Name = "Santo Pecado Burger",
                    Description = "La reina de la casa, la joya de la corona: SAAAANTO PECADO",
                    BasePrice = 10.25m,
                    ImageUrl = "img/burgers/santopecado-burger.png",
                },
                new BurgerSpecial()
                {
                    Id = 7,
                    Name = "Veggie Burger",
                    Description = "Llega la VEGGIE, una nueva burger vegetariana BRUUUUUTAL.",
                    BasePrice = 11.50m,
                    ImageUrl = "img/burgers/veggie-burger.png",
                },
                new BurgerSpecial()
                {
                    Id = 8,
                    Name = "Achilipu Burger",
                    Description = "Para esos días de hambre jurásica...",
                    BasePrice = 9.99m,
                    ImageUrl = "img/burgers/achilipu-burger.png",
                },
            };

            db.Toppings.AddRange(toppings);
            db.Specials.AddRange(specials);
            db.SaveChanges();
        }
    }
}
