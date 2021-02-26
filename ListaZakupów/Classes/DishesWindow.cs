using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ListaZakupów
{
    class DishesWindow
    {
        public static void addIngredientsToShoppingList(object sender, EventArgs eventArgs)
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

        public static void draw()
        {
            for (int i = 0; i < MainWindow.DISHES.Length; i++)
            {
                Dish dish = MainWindow.DISHES[i];
                int calories = dish.calculateCalories();
                BitmapImage image = Utils.getImageByName(dish.ImageName);

                DishListItem newItem = new DishListItem(dish.DishName, calories, dish.Ingredients, image);
                newItem.MouseDown += DishesWindow.addIngredientsToShoppingList;
                MainWindow.CONTENT_CONTAINER.Children.Add(newItem);
            }
        }
    }
}
