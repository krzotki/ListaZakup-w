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
    /// Logika interakcji dla klasy DishesWindowControl.xaml
    /// </summary>
    public partial class DishesWindow
    {
        public DishesWindow()
        {
            InitializeComponent();
            draw();
        }

        public void draw()
        {
            for (int i = 0; i < MainWindow.DISHES.Length; i++)
            {
                Dish dish = MainWindow.DISHES[i];
                int calories = dish.calculateCalories();
                BitmapImage image = Utils.getImageByName(dish.ImageName);

                DishListItem newItem = new DishListItem(dish.DishName, calories, dish.Ingredients, image);
                newItem.MouseDown += addIngredientsToShoppingList;
                contentPanel.Children.Add(newItem);
            }
        }

        public void addIngredientsToShoppingList(object sender, EventArgs eventArgs)
        {
            IngredientData[] ingredients = ((DishListItem)sender).Ingredients;

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
