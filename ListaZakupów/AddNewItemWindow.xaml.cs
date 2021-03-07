using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Logika interakcji dla klasy AddNewItemWindow.xaml
    /// </summary>
    public partial class AddNewItemWindow : UserControl
    {
        private Ingredient ingredientToAdd;
        private string ingredientImagePath;
        public AddNewItemWindow(string message = null, bool success = false)
        {
            InitializeComponent();
            openIngredientImage.Click += openImage;
            ingredientToAdd = new Ingredient();

            if (message != null)
            {
                setMessage(message, success);
            }
        }

        private void setMessage(string message, bool success)
        {
            if (success)
            {
                messageContainer.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF009900");
            }
            else
            {
                messageContainer.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF0000");
            }

            messageContainer.Content = message;
        }

        private void handleInputChange(object sender, EventArgs args)
        {
            TextBox input = (TextBox)sender;

            switch (input.Name)
            {
                case "ingredientName":
                    ingredientToAdd.IngredientName = input.Text;
                    break;
                case "ingredientPrice":
                    ingredientToAdd.Price = double.Parse(input.Text.ToString());
                    break;
                case "ingredientCalories":
                    ingredientToAdd.Calories = int.Parse(input.Text.ToString());
                    break;
            }

        }
        private void openImage(object sender, EventArgs args)
        {
            Button button = (Button)sender;

            OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "image";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string imageName = dlg.SafeFileName;
                string filePath = dlg.FileName;

                button.Content = imageName;

                if (button.Name == "openIngredientImage")
                {
                    ingredientToAdd.ImageName = imageName;
                    ingredientImagePath = filePath;

                    Uri imageUri = new Uri(filePath, UriKind.Absolute);
                    ingredientImagePreview.Source = new BitmapImage(imageUri);
                }
            }
        }

        private void saveIngredientToJSON()
        {
            Ingredient[] newIngredients = new Ingredient[MainWindow.INGREDIENTS.Length + 1];
            for (int i = 0; i < MainWindow.INGREDIENTS.Length; i++)
            {
                newIngredients[i] = MainWindow.INGREDIENTS[i];
            }

            newIngredients[newIngredients.Length - 1] = ingredientToAdd;

            string newJSON = JsonConvert.SerializeObject(newIngredients);

            FileStream stream = new FileStream("../../Data/ingredients.json", FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(stream, Encoding.Default);

            writer.Write(newJSON);
            writer.Close();

        }

        private void copyImage(string filePath, string imageName)
        {
            string targetPath = "../../images/";

            string destFile = System.IO.Path.Combine(targetPath, imageName);

            System.IO.File.Copy(filePath, destFile, true);
        }

        private void confirmAddIngredient(object sender, EventArgs args)
        {
            bool isGood = true;
            foreach (PropertyInfo prop in ingredientToAdd.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (prop.GetValue(ingredientToAdd, null) == null) isGood = false;
            }

            if (isGood)
            {
                saveIngredientToJSON();
                copyImage(ingredientImagePath, ingredientToAdd.ImageName);
                MainWindow.loadIngredientsFromJSON();
                MainWindow.CONTENT_CONTAINER.Content = new AddNewItemWindow("Dodano nowy produkt!", true);
            }
            else
            {
                setMessage("Uzupełnij wszystkie dane produktu", false);
            }
        }
    }
}
