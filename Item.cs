using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Refrigerator
{
    public class Item
    {
        //Shelf shelf = new Shelf();
        public enum Kashrot
        {
            Dairy, Meat, Fur
        }
        public enum TypeItem
        {
            Food, Drink
        }
        private static int _nextID = 1;

        public int _id = 0;
        private string _name;
        public Shelf? _shelfOfItem { get; set; }
        public Kashrot _kashrot { get; set; }
        public TypeItem _type { get; set; }
        public DateTime _expiryDate { get; set; }
        public int _size;


        public Item(string name, Kashrot kashrot, TypeItem typeFood, DateTime expiryDate, int size, Shelf shelfItem = null)
        {
            try
            {
                _id = _nextID++;
                setName(name);
                _shelfOfItem = shelfItem;
                _kashrot = kashrot;
                _type = typeFood;
                _expiryDate = expiryDate;
                setSize(size);
                _shelfOfItem = shelfItem;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }


        public int getId()
        {
            return _id;
        }
        public void setName(string name)
        {
            if(name.Length <= 0 || name.Length > 50)
            {
                throw new ArgumentException("Value must be non-negative and till 50 chars.", nameof(name));
            }
            _name = name;  
        }
        public void setSize(int size)
        {
            if (size<=0||size>1000)
            {
                throw new ArgumentException("Value must be non-negative and till 1000.", nameof(size));
            }
            _size = size;
        }

        public override string ToString()
        {
            //Console.WriteLine("Item Details:");
            int  msg;
            msg = (_shelfOfItem != null) ? _shelfOfItem.getId() : 0;
            return $"ID item: {_id}" + " " +               
                $"Name : {_name}" + " " +
                $"IdShelf: {msg}" + " " +
                $"Kashrot: {_kashrot}" + " " +
                $"Type: {_type}" + " " +
                $"Expiry Date: {_expiryDate.ToString("yyyy-MM-dd")}" + " " +
                $"size: {_size}";

        }
        public static List<Item> getSortedItems(List<Item> itemsToSort)
        {
            itemsToSort = itemsToSort.OrderBy(o => o._expiryDate).ToList();
            return itemsToSort;
        }
      



    }
}

