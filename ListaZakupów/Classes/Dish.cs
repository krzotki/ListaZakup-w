using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ListaZakupów
{

    struct Dish
    {
        string Name { get; }
        int Calories { get; }

        Ingredient[] Ingredients { get; }
        public Dish(string name, int calories, Ingredient[] ingredients)
        {
            this.Name = name;
            this.Calories = calories;
            this.Ingredients = ingredients;
        }

    }
    class DishListItem : ListItem
    {
        private int Calories { get; }
        public DishListItem(string productName, int calories, BitmapImage productImageSource)
            : base(productName + '\n' + calories + "kcal", productImageSource)
        {
            this.Calories = calories;

            this.Width = 100;
            this.Height = 100;
            this.Background = Brushes.Gray;
            this.Margin = new Thickness(10.0);

            this.label.Height = 40;
            this.label.Width = 100;
            this.label.HorizontalContentAlignment = HorizontalAlignment.Center;

            this.image.Width = 50;
            this.image.Height = 50;
            this.image.Margin = new Thickness(25.0, 0, 25.0, 0);
        }

    }
}
