﻿using System;
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
    /// Logika interakcji dla klasy ShoppingListItem.xaml
    /// </summary>
    public partial class ShoppingListItem : UserControl
    {
        public string IngredientName { get; }
        private int amount;
        public int Amount
        {
            get => amount;
            set
            {
                this.amount = value;
                this.label.Content = this.IngredientName + " x " + this.amount;
            }
        }
        public ShoppingListItem(string name, int amount, double price, BitmapImage productImageSource)
        {
            InitializeComponent();

            this.image.Source = productImageSource;
            this.IngredientName = name;
            this.Amount = amount;
        }
    }
}
