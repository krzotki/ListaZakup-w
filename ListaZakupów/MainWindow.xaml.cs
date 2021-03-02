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
        public static ListBox SHOPPING_LIST;
        public static ContentControl CONTENT_CONTAINER;
        public static Ingredient[] INGREDIENTS;
        public static Dish[] DISHES;
        private void loadIngredientsFromJSON()
        {
            string jsonText = File.ReadAllText("../../Data/ingredients.json", Encoding.Default);
            INGREDIENTS = JsonConvert.DeserializeObject<Ingredient[]>(jsonText);
        }

        private void loadDishesFromJSON()
        {
            string jsonText = File.ReadAllText("../../Data/dishes.json", Encoding.Default);
            DISHES = JsonConvert.DeserializeObject<Dish[]>(jsonText);
        }


        private void handleBookmarkClick(object sender, EventArgs args)
        {
        
            switch (((Button)sender).Name)
            {
                case "buttonDishes":
                    contentContainer.Content = new DishesWindow();
                    break;
                case "buttonIngredients":
                    contentContainer.Content = new IngredientsWindow();
                    break;
                case "buttonAddNew":
                    contentContainer.Content = new AddNewItemWindow();
                    break;
                case "buttonCalories":
                    contentContainer.Content = new CaloriesWindow();
                    break;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            loadIngredientsFromJSON();
            loadDishesFromJSON();
            SHOPPING_LIST = shoppingList;
            CONTENT_CONTAINER = contentContainer;

        }
    }
}
