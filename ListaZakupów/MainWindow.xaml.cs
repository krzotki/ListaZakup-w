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
        public MainWindow()
        {
            InitializeComponent();

            Uri pickleUri = new Uri("/images/pickle.jpg", UriKind.Relative);
            BitmapImage pickleImage = new BitmapImage(pickleUri);

            Uri mizeriaUri = new Uri("/images/mizeria.jpg", UriKind.Relative);
            BitmapImage mizeriaImage = new BitmapImage(mizeriaUri);

            Ingredient ogorek = new Ingredient("ogorek", 1.0, 2);

            /*
            Ingredient[] ings = { ogorek};

            Dish[] test = { new Dish("mizeria", 500, ings) };

            Stream fileStream = File.OpenWrite("../../Data/dishes.json");

            StreamWriter writer = new StreamWriter(fileStream);
            writer.Write(Utils.toJSON(test));
            writer.Close();*/

            string jsonText = File.ReadAllText("../../Data/dishes.json");

            Dish[] allDishes = JsonConvert.DeserializeObject<Dish[]>(jsonText);


            for (int i = 0; i < 20; i++)
            {
                shoppingList.Items.Add(new ShoppingListItem("Ogórek", i, pickleImage));
            }

            for (int i = 0; i < allDishes.Length; i++)
            {
                Dish dish = allDishes[i];
                dishesList.Children.Add(new DishListItem(dish.Name, dish.Calories, mizeriaImage));
            }
        }
    }
}
