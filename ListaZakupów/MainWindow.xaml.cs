using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        private void addIngredientsToShoppingList(object sender, EventArgs eventArgs)
        {
            IngredientData[] ingredients = ((DishListItem)sender).Ingredients;

            for (int i = 0; i < ingredients.Length; i++)
            {
                Ingredient ingredient = ingredients[i].getDetails();
                int amount = ingredients[i].Amount;

                BitmapImage image = Utils.getImageByName(ingredient.ImageName);

                ShoppingListItem item = new ShoppingListItem(ingredient.IngredientName, amount, ingredient.Price, image);

                ShoppingListItem oldItem = Utils.findItemInShoppingList(item, shoppingList.Items);

                if (oldItem != null)
                {
                    oldItem.Amount += item.Amount;
                }
                else
                {
                    shoppingList.Items.Add(item);
                }
            }
        }

        public static Ingredient[] INGREDIENTS;
        public static Dish[] DISHES;
        public void loadIngredientsFromJSON()
        {
            string jsonText = File.ReadAllText("../../Data/ingredients.json", Encoding.Default);
            INGREDIENTS = JsonConvert.DeserializeObject<Ingredient[]>(jsonText);
        }

        public void loadDishesFromJSON()
        {
            string jsonText = File.ReadAllText("../../Data/dishes.json", Encoding.Default);
            DISHES = JsonConvert.DeserializeObject<Dish[]>(jsonText);
        }

        public void drawDishes()
        {
            for (int i = 0; i < DISHES.Length; i++)
            {
                Dish dish = DISHES[i];
                int calories = dish.calculateCalories();
                BitmapImage image = Utils.getImageByName(dish.ImageName);

                DishListItem newItem = new DishListItem(dish.DishName, calories, dish.Ingredients, image);
                newItem.MouseDown += addIngredientsToShoppingList;
                contentContainer.Children.Add(newItem);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            loadIngredientsFromJSON();
            loadDishesFromJSON();

            drawDishes();
            
        }
    }
}
