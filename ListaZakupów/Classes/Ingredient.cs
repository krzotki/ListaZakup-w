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

        private string productName;
        private int amount;
        public int Amount
        {
            get => amount;
            set
            {
                this.amount = value;
                this.label.Content = this.productName + " x " + this.amount;
            }
        }
        public ShoppingListItem(string productName, int amount, BitmapImage productImageSource) : base(productName, productImageSource)
        {
            this.productName = productName;
            this.Amount = amount;
            this.image.Width = 50;
            this.image.Height = 50;
        }
    }

    struct Ingredient
    {
        string Name { get; }
        double Price { get; }
        int Amount { get; set; }

        public Ingredient(string name, double price, int amount)
        {
            this.Name = name;
            this.Price = price;
            this.Amount = amount;
        }
    }
}
