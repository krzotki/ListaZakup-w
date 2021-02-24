using Newtonsoft.Json;
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

            Ingredient[] ings = { ogorek};

            Dish test = new Dish("mizeria", 500, ings);
            string json = JsonConvert.SerializeObject(test);

            for (int i = 0; i < 20; i++)
            {

                shoppingList.Items.Add(new ShoppingListItem("Ogórek", i, pickleImage));
                dishesList.Children.Add(new DishListItem("Mizeria", 500, mizeriaImage));
            }
        }
    }
}
