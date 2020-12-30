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
            // Console.WriteLine("Hello World!");      
            int dog = 0;
            string dirName = "./Data";
            string fileName = dirName + "/APIData.txt";
            string prettyFileName = dirName + "/PrettyAPIData.txt";

            if (File.Exists(prettyFileName)) File.Delete(prettyFileName);            
          

            // should only need to be called once
            List<Item> itemList = new();
            itemList = InitialDBLoad();            
            WriteListtoFile(itemList);
            //List<Item> ItemListFromDisk = ReadListFromFile();

            // insert a record
            InsertItemintoList("Coke", 2.25, 2.25);

            // update a record
            UpdateItemInListById(5, "Pepsi", 2.5, 2.5);

            // delete a record
            DeleteItemfromListByName("Pepsi");

            dog++;

        } // end of main method

        public static void UpdateItemInListById(int _id, string _name, double _price, double _rating)
        {

            // read list from file
            List<Item> _ItemListFromDisk = ReadListFromFile();

            if (_ItemListFromDisk.Exists(x => x.Id == _id))
            {
                Item _ItemtoUpdate = _ItemListFromDisk.Find(p => p.Id == _id);

                _ItemtoUpdate.Name = _name;
                _ItemtoUpdate.Price = _price;
                _ItemtoUpdate.Rating = _rating;

                // remove item from list   
                _ItemListFromDisk.RemoveAll(p => p.Id == _id);

                // add record to list with values from put
                _ItemListFromDisk.Add(_ItemtoUpdate);

            }

            //write list to file
            WriteListtoFile(_ItemListFromDisk);


        }

        public static void DeleteItemfromListByName(string _name)
        {

            // read list from file
            List<Item> _ItemListFromDisk = ReadListFromFile();

            // find item in list
            // findindex of the item based on name search
            // remove based on index            

            //_ItemListFromDisk.Find(new Item(){Name = _name });            

            if (_ItemListFromDisk.Exists(x => x.Name == _name))
            {
                // remove item from list   
                _ItemListFromDisk.RemoveAll(p => p.Name == _name);

                //_ItemListFromDisk.Remove(new Item() { Name = _name });

                //_ItemListFromDisk.Remove(_ItemListFromDisk[3].Name = _name );

            }

            //write list to file
            WriteListtoFile(_ItemListFromDisk);

        }
        public static void InsertItemintoList(string _name, double _price, double _rating)
        {
            // read list from file
            List<Item> _ItemListFromDisk = ReadListFromFile();

            // get max id from list
            Item _LastItem = new();
            int _nextItemID = 1;

            if (_ItemListFromDisk.Count > 0)
            {
                _LastItem = _ItemListFromDisk[_ItemListFromDisk.Count - 1];
                _nextItemID = _LastItem.Id + 1;
            }

            // add record to list with values from put
            _ItemListFromDisk.Add(new Item { Id = _nextItemID, Name = _name, Price = _price, Rating = _rating });

            // write list to file
            WriteListtoFile(_ItemListFromDisk);

        }

        public static List<Item> InitialDBLoad()
        {
            List<Item> _itemList = new();
            _itemList.Add(new Item() { Id = 1, Name = "Martini", Price = 15.25, Rating = 4.8 });
            _itemList.Add(new Item() { Id = 2, Name = "Manhattan", Price = 15.50, Rating = 4.5 });
            _itemList.Add(new Item() { Id = 3, Name = "Domestic Beer", Price = 8.50, Rating = 3.0 });
            _itemList.Add(new Item() { Id = 4, Name = "Wine", Price = 10.00, Rating = 3.5 });

            return _itemList;

        }

        public static List<Item> ReadListFromFile()
        {
            // in method variable
            List<Item> _items = new();

            // read file
            string dirName = "./Data";
            string fileName = dirName + "/APIData.txt";
            string jsonString = File.ReadAllText(fileName);

            // deserialize into class array
            //var options = new JsonSerializerOptions { WriteIndented = true, };
            //_items  = JsonSerializer.Deserialize<List<Item>>(jsonString, options);
            _items = JsonSerializer.Deserialize<List<Item>>(jsonString);

            // pass list back to caller

            //_items.Sort(); 
            return _items;


        }

        public static void WriteListtoFile(List<Item> _items)
        {

            //below two lines are for debugging needs only
            var options = new JsonSerializerOptions { WriteIndented = true, };
            string jsonStringPretty = JsonSerializer.Serialize(_items, options);

            string jsonString = JsonSerializer.Serialize(_items);

            string dirName = "./Data";
            string fileName = dirName + "/APIData.txt";
            string prettyFileName = dirName + "/PrettyAPIData.txt";

            // Create Directory creates dir if it does not exists, otherwise it does nothing
            Directory.CreateDirectory(dirName);

            //if (!File.Exists(fileName)) File.Create()
            
            

            using FileStream APIDataFileStream = File.Open(fileName, FileMode.OpenOrCreate);
            APIDataFileStream.SetLength(0);

            //File.WriteAllText(fileName, jsonString);

            byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
            APIDataFileStream.Write(info, 0, info.Length);

            using FileStream APIDataFileStreamPretty = File.Open(prettyFileName, FileMode.Append);
            info = new UTF8Encoding(true).GetBytes(jsonStringPretty);
            APIDataFileStreamPretty.Write(info, 0, info.Length);

            APIDataFileStream.Close();

        }





    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }

    }

}
