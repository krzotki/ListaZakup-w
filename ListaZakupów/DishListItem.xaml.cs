using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListaZakupów
{
    /// <summary>
    /// Logika interakcji dla klasy DishListItem.xaml
    /// </summary>
    public partial class DishListItem : UserControl
    {
        public int Calories { get; }
        public IngredientData[] Ingredients { get; }
        public DishListItem(string productName, int calories, IngredientData[] ingredients, BitmapImage productImageSource)
        {
            InitializeComponent();

            this.label.Content = productName + '\n' + calories + " kcal";
            this.image.Source = productImageSource;

            this.Calories = calories;
            this.Ingredients = ingredients;

            this.setTooltipIngredients();
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
