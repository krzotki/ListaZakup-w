using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaZakupów
{
    class CaloriesData
    {
        public static List<CaloriesData> AllData = new List<CaloriesData>();
        public double Calories { get; set; }
        public long Date { get; set; }

        public DateTime FormattedDate { get; set; }

        public CaloriesData(long date, double calories)
        {
            this.Calories = calories;
            this.Date = date;
            this.FormattedDate = Utils.UnixTimeStampToDateTime(date);
        }

        public static void loadFromJSON()
        {
            string jsonText = File.ReadAllText("../../Data/calories.json", Encoding.Default);
            AllData = JsonConvert.DeserializeObject<List<CaloriesData>>(jsonText);

            initJSON();
        }

        private static void initJSON()
        {
            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            int dayLength = 60 * 60 * 24;

            if (AllData.Count == 0)
            {
                for (int i = 0; i < 30; i++)
                {
                    CaloriesData newItem = new CaloriesData(timestamp - i * dayLength, 0);
                    AllData.Insert(0, newItem);
                }
            }

            addData(timestamp, 0);

            saveToJSON();
        }

        public static void addData(long timestamp, double calories) 
        {
            int dayLength = 60 * 60 * 24;

            while (timestamp - dayLength > AllData[AllData.Count - 1].Date) 
            {
                AllData.RemoveAt(0);
                AllData.Add(new CaloriesData(AllData[AllData.Count - 1].Date + dayLength, 0));
            }

            CaloriesData newItem = new CaloriesData(timestamp, calories);

            if (AllData[AllData.Count - 1].FormattedDate.Day == newItem.FormattedDate.Day)
            {
                AllData[AllData.Count - 1].Calories += newItem.Calories;
            }
            else 
            {
                AllData.RemoveAt(0);
                AllData.Add(newItem);
            }

            saveToJSON();
        }

        public static void saveToJSON()
        {
            string newJSON = JsonConvert.SerializeObject(AllData, Formatting.Indented);

            FileStream stream = new FileStream("../../Data/calories.json", FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(stream, Encoding.Default);

            writer.Write(newJSON);
            writer.Close();
        }


    }
}
