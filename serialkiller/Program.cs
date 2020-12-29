using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace serialkiller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            Cocktail cocktailTim = new();
            cocktailTim.Id = 1;
            cocktailTim.Name = "Martini";
            cocktailTim.Price = 15.25;
            cocktailTim.Rating = 4.8;


            List<Cocktail> cocktailList = new();
            cocktailList.Add(new Cocktail() { Id = 1, Name = "Martini", Price = 15.25, Rating = 4.8 });
            cocktailList.Add(new Cocktail() { Id = 2, Name = "Manhattan", Price = 15.50, Rating = 4.5 });
            cocktailList.Add(new Cocktail() { Id = 3, Name = "Domestic Beer", Price = 8.50, Rating = 3.0 });
            cocktailList.Add(new Cocktail() { Id = 4, Name = "Wine", Price = 10.00, Rating = 3.5 });
            WriteClasstoFile(cocktailList);

            List<Cocktail> cocktailListFromDisk = new();
            LoadClassFromFile(cocktailListFromDisk);


        } // end of main method

        public static void LoadClassFromFile(List<Cocktail> _cocktail)
        {
            
            
            // read file
            string dirName = "./Data";
            string fileName = dirName + "/APIData.txt";
            string jsonString = File.ReadAllText(fileName);             

            // deserialize into class array
            var options = new JsonSerializerOptions { WriteIndented = true, };

            _cocktail  = JsonSerializer.Deserialize<List<Cocktail>>(jsonString);

            // pass list back to caller

            

        }

        public static void WriteClasstoFile(List<Cocktail> _cocktail)
        {

            var options = new JsonSerializerOptions { WriteIndented = true, };

            //string jsonString = JsonSerializer.Serialize(_cocktail, options);
            string jsonString = JsonSerializer.Serialize(_cocktail);

            string dirName = "./Data";
            string fileName = dirName + "/APIData.txt";

            // Create Directory creates dir if it does not exists, otherwise it does nothing
            Directory.CreateDirectory(dirName);

            using FileStream APIDataFileStream = File.Open(fileName, FileMode.Append);

            byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
            APIDataFileStream.Write(info, 0, info.Length);

        }





    }

    public class Cocktail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }

    }

}
