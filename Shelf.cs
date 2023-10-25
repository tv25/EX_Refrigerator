using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Refrigerator.Item;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using System.Data;

namespace Refrigerator
{

    public class Shelf
    {
        private static int _nextID = 1;

        private int _id = 0;
        private int _shelfFloorNumber { get; set; }
        private int _size { get; set; }
        public List<Item> _itemsOnShelf { get; set; }

        public Shelf(int shelfFloorNumber, int size, List<Item> itemsOnShelf)
        {
            _id = _nextID++;
            _shelfFloorNumber = 0;
            _size = size;
            _itemsOnShelf = itemsOnShelf;
            _shelfFloorNumber = shelfFloorNumber;

        }
        public int getId() { return _id; }
        public override string ToString()
        {

            return $"ID shelf : {_id}" + " " +
                $"Floor Shelfe : {_shelfFloorNumber}" + " " +
                $"Size: {_size}";
        }

        public int freeSpaceInShelf(Shelf shelf)
        {

            int sumOfAllItems = shelf._itemsOnShelf.Sum(i => i._size);
            int x = ((shelf._size - sumOfAllItems) <= 0) ? 0 : shelf._size - sumOfAllItems;
            return x;
        }

        public void addItemToShelf(Item item)
        {
            if (freeSpaceInShelf(this) >= item._size)
            {
                _itemsOnShelf.Add(item);
                item._idShelf = this._id;
                return;

            }
            Console.WriteLine("Sorrt,there is no enough place to add this item");

        }

        public Item takeOffItemFromShelf(int idItem)
        {
            Item foundItem = _itemsOnShelf.Find(i => i._id == idItem);
            if (foundItem != null)
            {
                _itemsOnShelf.Remove(foundItem);
                foundItem._idShelf = 0;
                return foundItem;
            }
            return null;

        }


        public List<Item> getExpiredItem()
        {
            List<Item> expiredItems = _itemsOnShelf.Where(item => item._expiryDate < DateTime.Now).ToList();
            return expiredItems;

        }
        public List<Item> getYourFood(Kashrot kashrot, TypeItem typeItem)

        {
            List<Item> yourItems = new List<Item>();
            yourItems = (_itemsOnShelf.FindAll(i => i._kashrot == kashrot && i._type == typeItem && i._expiryDate >= DateTime.Now).ToList());
            return yourItems;
        }

        public List<Item> getItemeByValues(Kashrot kashrot, DateTime specifyDate)
        {
            List<Item> foundtems = new List<Item>();
            foundtems = (_itemsOnShelf.FindAll(i => i._kashrot == kashrot && i._expiryDate < specifyDate).ToList());
            return foundtems;
        }
    }
}


