using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ListaZakupów
{
    public struct IngredientData
    {
        public int Index { get; }
        public int Amount { get; }

        public IngredientData(int index, int amount)
        {
            this.Index = index;
            this.Amount = amount;
        }

        public Ingredient getDetails()
        {
            return MainWindow.INGREDIENTS[this.Index];
        }
    }
    public class Dish
    {
        public string DishName { get; }
        public IngredientData[] Ingredients { get; }
        public string ImageName { get; }
        public Dish(string dishName, IngredientData[] ingredients, string imageName)
        {
            this.DishName = dishName;
            this.Ingredients = ingredients;
            this.ImageName = imageName;
        }

        public int calculateCalories()
        {
            int sum = 0;
            for (int i = 0; i < this.Ingredients.Length; i++)
            {
                int index = this.Ingredients[i].Index;
                int amount = this.Ingredients[i].Amount;

                int calories = MainWindow.INGREDIENTS[index].Calories * amount;
                sum += calories;
            }

            return sum;
        }
    }

    class DishListItem : ListItem
    {
        public int Calories { get; }
        public IngredientData[] Ingredients { get;}
        public DishListItem(string productName, int calories, IngredientData[] ingredients, BitmapImage productImageSource)
            : base(productName + '\n' + calories + "kcal", productImageSource)
        {
            this.Calories = calories;
            this.Ingredients = ingredients;

            this.setTooltipIngredients();
            this.Width = 200;
            this.Height = 200;
            this.Background = Brushes.Gray;
            this.Margin = new Thickness(10.0);

            this.label.FontSize = 16;
            this.label.Foreground = Brushes.White;
            this.label.Height = 60;
            this.label.Width = 200;
            this.label.HorizontalContentAlignment = HorizontalAlignment.Center;

            this.image.Width = 130;
            this.image.Height = 130;
            this.image.Margin = new Thickness(35, 0, 35, 0);
        }

        private void setTooltipIngredients()
        {
            string text = "Składniki: \n";
            for (int i = 0; i < this.Ingredients.Length; i++)
            {
                IngredientData ingredient = this.Ingredients[i];
                int amount = ingredient.Amount;
                Ingredient details = ingredient.getDetails();

                text += amount + " x " + details.IngredientName + " (" + details.Calories + " kcal)\n";
            }

            this.ToolTip = text;
        }
    }
}
