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
    public class DishesWindow : WrapPanel
    {
        public DishesWindow() : base()
        {
            this.Width = Double.NaN;
            this.Height = Double.NaN;
            this.Background = Brushes.White;
            draw();
        }

        public void draw()
        {
            for (int i = 0; i < MainWindow.DISHES.Length; i++)
            {
                Dish dish = MainWindow.DISHES[i];
                int calories = dish.calculateCalories();
                double price = dish.calculatePrice();
                BitmapImage image = Utils.getImageByName(dish.ImageName);

                DishListItem newItem = new DishListItem(dish.DishName, calories, price, dish.Ingredients, image);
                this.Children.Add(newItem);
            }
        }
    }
}
