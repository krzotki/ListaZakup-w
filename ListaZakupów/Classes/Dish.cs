﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ListaZakupów
{
    public struct IngredientData
    {
        public int Index { get; }
        public int Amount { get; }

        public IngredientData(int index, int amount)
        {
            this.Index = index;
            this.Amount = amount;
        }

        public Ingredient getDetails()
        {
            return MainWindow.INGREDIENTS[this.Index];
        }
    }
    public struct Dish
    {
        public string DishName { get; }
        public IngredientData[] Ingredients { get; }
        public string ImageName { get; }
        public Dish(string dishName, IngredientData[] ingredients, string imageName)
        {
            this.DishName = dishName;
            this.Ingredients = ingredients;
            this.ImageName = imageName;
        }

        public int calculateCalories()
        {
            int sum = 0;
            for (int i = 0; i < this.Ingredients.Length; i++)
            {
                int index = this.Ingredients[i].Index;
                int amount = this.Ingredients[i].Amount;

                int calories = MainWindow.INGREDIENTS[index].Calories * amount;
                sum += calories;
            }

            return sum;
        }
    }
}
