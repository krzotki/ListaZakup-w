using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace ListaZakupów
{
    class Utils
    {
        static public string toJSON(Object objectInstance)
        {
            return JsonConvert.SerializeObject(objectInstance);
        }

        public static ShoppingListItem findItemInShoppingList(ShoppingListItem item, ItemCollection list)
        {
            foreach (ShoppingListItem listItem in list)
            {
                if (item.IngredientName == listItem.IngredientName)
                {
                    return listItem;
                }
            }

            return null;
        }

        public static BitmapImage getImageByName(string name)
        {

            Uri imageUri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "../../images/" + name, UriKind.Absolute);
            return new BitmapImage(imageUri);
        }
    }
}
