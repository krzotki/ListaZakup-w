using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ListaZakupów
{
    public struct Ingredient
    {
        public string IngredientName { get; set; }
        public double Price { get; set; }
        public int Calories { get; set; }
        public string ImageName { get; set; }

        public Ingredient(string ingredientName, double price, int calories, string imageName)
        {
            this.IngredientName = ingredientName;
            this.Price = price;
            this.Calories = calories;
            this.ImageName = imageName;
        }

    }
}
