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

        public static string shoppingListToString(ItemCollection list, double price)
        {
            string result = "";
            foreach (ShoppingListItem listItem in list)
            {
                string name = listItem.IngredientName;
                int amount = listItem.Amount;
                result += string.Format("{0} x {1}\n", name, amount);
            }

            result += string.Format("\nRazem: {0} zł", price);

            return result;
        }

        public static IngredientData[] shoppingListToIngredientData(ItemCollection list)
        {
            IngredientData[] ingredientData = new IngredientData[list.Count];
            
            for (int i = 0; i < list.Count; i++)
            {
                ShoppingListItem item = (ShoppingListItem)list.GetItemAt(i);
                ingredientData[i] = new IngredientData(item.Index, item.Amount);
            }

            return ingredientData;
        }

        public static BitmapImage getImageByName(string name)
        {

            Uri imageUri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "../../images/" + name, UriKind.Absolute);
            return new BitmapImage(imageUri);
        }
    }
}
