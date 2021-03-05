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
            this.MouseDown += this.addIngredientsToShoppingList;
            this.MouseEnter += this.onMouseOver;
            this.MouseLeave += this.onMouseOut;
        }

        private void onMouseOver(object sender, EventArgs args)
        { 
            this.Background = (Brush)new BrushConverter().ConvertFromString("#119da4");
        }

        private void onMouseOut(object sender, EventArgs args)
        {
            this.Background = (Brush)new BrushConverter().ConvertFromString("#0c7489");
        }

        public static DishListItem fromIngredient(int i)
        {
            Ingredient ingredient = MainWindow.INGREDIENTS[i];
            int calories = ingredient.Calories;
            BitmapImage image = Utils.getImageByName(ingredient.ImageName);

            IngredientData[] data = new IngredientData[1];
            data[0] = new IngredientData(i, 1);

            DishListItem ingredientListItem = new DishListItem(ingredient.IngredientName, calories, data, image);
            ingredientListItem.ToolTip = null;
            return ingredientListItem;
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

        private void addIngredientsToShoppingList(object sender, EventArgs eventArgs)
        {
            IngredientData[] ingredients = this.Ingredients;

            for (int i = 0; i < ingredients.Length; i++)
            {
                Ingredient ingredient = ingredients[i].getDetails();
                int amount = ingredients[i].Amount;

                BitmapImage image = Utils.getImageByName(ingredient.ImageName);

                ShoppingListItem item = new ShoppingListItem(ingredient.IngredientName, amount, ingredient.Price, image);

                ShoppingListItem oldItem = Utils.findItemInShoppingList(item, MainWindow.SHOPPING_LIST.Items);

                if (oldItem != null)
                {
                    oldItem.Amount += item.Amount;

                }
                else
                {
                    MainWindow.SHOPPING_LIST.Items.Add(item);
                }
            }
        }
    }
}
