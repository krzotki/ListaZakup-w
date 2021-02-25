using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ListaZakupów
{
    class ShoppingListItem : ListItem
    {

        public string IngredientName { get; }
        private int amount;
        public int Amount
        {
            get => amount;
            set
            {
                this.amount = value;
                this.label.Content = this.IngredientName + " x " + this.amount;
            }
        }
        public ShoppingListItem(string name, int amount, double price, BitmapImage productImageSource) : base(name, productImageSource)
        {
            this.IngredientName = name;
            this.Amount = amount;
            this.image.Width = 50;
            this.image.Height = 50;
        }
    }

    public struct Ingredient
    {
        public string IngredientName { get; }
        public double Price { get; }
        public int Calories { get; }
        public string ImageName { get; }

        public Ingredient(string ingredientName, double price, int calories, string imageName)
        {
            this.IngredientName = ingredientName;
            this.Price = price;
            this.Calories = calories;
            this.ImageName = imageName;
        }
    }
}
