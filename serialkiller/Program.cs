using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace serialkiller
{
    class Program
    {
        static void Main(string[] args)
        {
            int age = 5;
            int catchage=0;
            Console.WriteLine("Hello World!");
            Cocktail cocktailTim = new();
            cocktailTim.Id = 1;
            cocktailTim.Name = "Manhattan";
            cocktailTim.Price = 15.25;
            cocktailTim.Rating = 4.8;

            catchage = TestMethod(age);
            WriteClasstoFile(cocktailTim);

            int TestMethod(int dog)
            {
                dog++;
                return dog;
            }

            void WriteClasstoFile(Cocktail _cocktail)
            {
                string jsonString = JsonSerializer.Serialize(_cocktail);
                string fileName = "./Data/APIData.txt";
                using FileStream createStream = File.Create(fileName);
                File.WriteAllText(fileName, jsonString);
                //JsonSerializer.SerializeAsync(createStream, jsonString);

            }
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
