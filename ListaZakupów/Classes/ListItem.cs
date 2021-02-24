using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ListaZakupów
{
    class ListItem : WrapPanel
    {
        protected Label label;
        protected Image image;

        public ListItem(string text, BitmapImage imageSrc) : base()
        {
            this.label = new Label();
            this.label.Content = text;
            this.Children.Add(this.label);

            this.image = new Image();
            this.image.Source = imageSrc;
            this.Children.Add(this.image);
        }

    }
}
