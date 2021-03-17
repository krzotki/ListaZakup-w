using Microsoft.Win32;
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

        private double caloriesSum = 0;
        private double priceSum = 0;

        public void addCalories(double calories)
        {
            caloriesSum += calories;
            caloriesSumLabel.Content = caloriesSum + " kcal";
        }
        public void addPrice(double price)
        {
            priceSum += price;
            priceSumLabel.Content = priceSum + " zł";
        }

        private bool saveShoppingListToFile()
        {
            SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.FileName = "Lista zakupów";
            saveDialog.DefaultExt = ".txt";
            saveDialog.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = saveDialog.ShowDialog();

            if (result == true)
            {
                string textContent = Utils.shoppingListToString(shoppingList.Items, priceSum);
                Stream fileStream = saveDialog.OpenFile();

                StreamWriter writer = new StreamWriter(fileStream);
                writer.Write(textContent);
                writer.Close();

                return true;
            }

            return false;
        }

        private void clearShoppingList()
        {
            caloriesSum = 0;
            priceSum = 0;
            addCalories(0);
            addPrice(0);

            shoppingList.Items.Clear();
        }

        public static void loadIngredientsFromJSON()
        {
            string jsonText = File.ReadAllText("../../Data/ingredients.json", Encoding.UTF8);
            INGREDIENTS = JsonConvert.DeserializeObject<Ingredient[]>(jsonText);
        }

        public static void loadDishesFromJSON()
        {
            string jsonText = File.ReadAllText("../../Data/dishes.json", Encoding.Default);
            DISHES = JsonConvert.DeserializeObject<Dish[]>(jsonText);
        }

        private void highlightButton(string name)
        {
            foreach(Button button in bookmarks.Children)
            {
                if (button.Name == name)
                {
                    button.Background = (Brush)new BrushConverter().ConvertFromString("#FFFFFF");
                }
                else
                {
                    button.Background = (Brush)new BrushConverter().ConvertFromString("#BBBBBB");
                }
            }
        }

        private void handleBookmarkClick(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            switch (button.Name)
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

            this.highlightButton(button.Name);
           
        }



        public MainWindow()
        {
            InitializeComponent();

            loadIngredientsFromJSON();
            loadDishesFromJSON();
            CaloriesData.loadFromJSON();

            SHOPPING_LIST = shoppingList;
            CONTENT_CONTAINER = contentContainer;
        }

        private void saveShoppingList(object sender, RoutedEventArgs e)
        {
            if (saveShoppingListToFile()) {
                CaloriesData.addData(Utils.getTodayTimestamp(), caloriesSum);
                clearShoppingList();
            }

        }
    }
}
