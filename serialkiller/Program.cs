using System;
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
            cocktailTim.Name = "Roberts";
            cocktailTim.Price = 15.25;
            cocktailTim.Rating = 4.8;
            
            WriteClasstoFile(cocktailTim);

        } // end of main method

        

        public static void WriteClasstoFile(Cocktail _cocktail)
        {
            
            string jsonString = JsonSerializer.Serialize(_cocktail);
            jsonString = jsonString + "\n";
            string dirName =  "./Data";
            string fileName = dirName + "/APIData.txt";            

            // Create Directory creates dir if it does not exists, otherwise it does nothing
            Directory.CreateDirectory(dirName); 
            
            using FileStream APIDataFileStream = File.Open(fileName,FileMode.Append);

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
