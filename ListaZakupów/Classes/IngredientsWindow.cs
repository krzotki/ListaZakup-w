using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ListaZakupów
{
    class IngredientsWindow : WrapPanel
    {
        public IngredientsWindow() : base()
        {
            this.Width = Double.NaN;
            this.Height = Double.NaN;
            this.Background = Brushes.White;
            draw();
        }
        public void draw()
        {
            for (int i = 0; i < MainWindow.INGREDIENTS.Length; i++)
            {
                DishListItem newItem = DishListItem.fromIngredient(i);
                this.Children.Add(newItem);
            }
        }
    }

}
