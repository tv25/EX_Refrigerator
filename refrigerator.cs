using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Refrigerator.Item;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Linq.Expressions;
using System.Collections;
using System.Diagnostics;

namespace Refrigerator
{
    public class Refrigerator
    {
        public enum Colors
        {
            Aquamarine, Azure, BurlyWood, CadetBlue, Gainsboro, Gold, Gray, Khaki, LawnGreen, LightGreen, LightSkyBlue,
            Linen, MediumOrchid, MediumPurple, MistyRose, Olive, Orange, Pink, Plum

        };
        public enum Model
        {
            LG_GR_X720INS, Haier_HRF839SS, LUXOR_NF535INOX, LG_GR_X265, Samsung_RL4324, Haier_HRF4556,
            LG_GR_J710, Blomberg_KND3954XP, Sharp_SJ_SE70D

        };
        public static int _nextID = 1;

        private int _id = 0;
        private Model _model { get; set; }
        private Colors _color { get; set; }
        private int _numOfShelf { get; set; }

        public List<Shelf> _shelfes { get; set; }


        public Refrigerator(Model model, Colors color, int numofshelf, List<Shelf> shelfes)
        {
            _id = _nextID++;
            _model = model;
            _color = color;
            _numOfShelf = numofshelf;
            _shelfes = shelfes;
        }
        public int getId() { return _id; }
        public override string ToString()
        {

            //string itemStr = string.Join("\n", _shelfes);

            return $"ID Refriger : {_id}" + " " +
                 $"Color : {_color}" + " " +
                  $"Number Shelfes : {_numOfShelf}" + " " +
                   $"Model: {_model}";
                // $"Model: {_model}\n{itemStr}";

        }

        public int freeSpaceInRefrigerator()
        {
            int freeSpace = 0;
             freeSpace=_shelfes.Sum(s => s.freeSpaceInShelf(s));
            return freeSpace;
        }

        public void insertItemToFriger(Item item)
        {
            if (item._shelfOfItem == null)
            {
                Shelf freeShelf = _shelfes.Find(s => s.freeSpaceInShelf(s) >= item._size);
                if (freeShelf != null)
                {
                    freeShelf.addItemToShelf(item);
                    Console.WriteLine("Your item insert to the friger.");

                }
                else
                {
                    Console.WriteLine("Sorry,there is no enouge place to insert this item");
                }

            }
            else
            {
                Console.WriteLine("Sorry,this item already in the friger");
            }

        }
        
        public object takeOffItemFromFriger(int idItem)
        {
  
            foreach (Shelf shelf in _shelfes)
            {
                Item itemToDelet = shelf.takeOffItemFromShelf(idItem);
                if (itemToDelet != null)
                {
                    string msg = "This item is remove from the refrigerator:" + "\n";
                    return msg + itemToDelet.ToString();
                }
            }
            return "Sorry,this item is not in the friger ,I cant take it out";

        }

        public void deletExpiredItems(List<Item> allItem)
        {
            List<Item> expiredItems = new List<Item>();
            _shelfes.ForEach(s => expiredItems.AddRange(s.getExpiredItem()));
            if(expiredItems.Count > 0)
            {
                deletItems(allItem, expiredItems);
                Console.WriteLine("All expired products were thrown away");
            }
            else
            {
                Console.WriteLine("Sorry, there is no expired item");
            }
           
        }
        public void deletItems(List<Item> allItem,List<Item>second)
        {
            second.ForEach(i => takeOffItemFromFriger(i._id));
            allItem.RemoveAll(item => second.Contains(item));


        }

        public List<Item> getYourFoods(Kashrot kashrot, TypeItem typeItem)
        {
            List<Item> yourItems = new List<Item>();
            foreach (Shelf shelf in _shelfes)
            {
                yourItems.AddRange(shelf.getYourFood(kashrot, typeItem));
            }
            ;

            return yourItems;
        }
        
        public List<Shelf> sortShelfesInFrige()
        {
            List<Shelf> items = new List<Shelf>();
            items= _shelfes.OrderByDescending(r => r.freeSpaceInShelf(r)).ToList();
            return items;
        }
        public static List<Refrigerator> sortRefriger(List<Refrigerator> r)
        {
            List<Refrigerator> Refriger = new List<Refrigerator>();
            Refriger = r.OrderByDescending(i =>i.freeSpaceInRefrigerator()).ToList();
            return Refriger;
        }
        public int getSumOfSizeItem(List<Item> items) {

            return  items.Sum(item => item._size);
        }
       
        public List<Item> getItemesByValues(Kashrot kashrot,DateTime specifyDate)
        {
            List<Item> foudItems = new List<Item>();
            foreach (Shelf shelf in _shelfes)
            {
                foudItems.AddRange(shelf.getItemeByValues(kashrot, specifyDate));
            }
            return foudItems;
        }

         public void getReadyForShopping(List<Item>allItems)
         {
            List<Item> allItemsCanDelet = new List<Item>();
            int sumAllItemsCanDelet,i = 0;
            int freeSpace = freeSpaceInRefrigerator();

            Dictionary<Kashrot, DateTime> kashrotAndTipe = new Dictionary<Kashrot, DateTime>();
            kashrotAndTipe.Add(Kashrot.Dairy, DateTime.Now.AddDays(3));
            kashrotAndTipe.Add(Kashrot.Meat, DateTime.Now.AddDays(7));
            kashrotAndTipe.Add(Kashrot.Fur, DateTime.Now.AddDays(1));

            int processNumberToThrowingFood = 1;
            int place = 20;
            if (freeSpaceInRefrigerator() >= 20)
            {
                Console.WriteLine("You can do shopping,there is enough place");
                return;
            }


             while (freeSpace < place && processNumberToThrowingFood != 5)
             {
                 switch (processNumberToThrowingFood)
                 {
                     case 1:

                         deletExpiredItems(allItems);
                        freeSpace = freeSpaceInRefrigerator();
                        processNumberToThrowingFood = 2;
                         break;
                     case 2:
                        for(int times = 0; times < 3; times++) {
                            allItemsCanDelet.AddRange(getItemesByValues(kashrotAndTipe.ElementAt(i).Key, kashrotAndTipe.ElementAt(i).Value).ToList());
                            sumAllItemsCanDelet = getSumOfSizeItem(allItemsCanDelet);
                            if(freeSpace+ sumAllItemsCanDelet >= place)
                            {
                                deletItems(allItems, allItemsCanDelet);
                                freeSpace += sumAllItemsCanDelet;
                                break;
                            }
                            i += 1;
                            processNumberToThrowingFood += 1;
                        }

                         break;                    
                     default:
                        
                        break ;

                 }
             }
            if (freeSpace >= place && processNumberToThrowingFood == 2)
            {
                Console.WriteLine("We threw away all expired foods");
            }
            else if (freeSpace >= place && processNumberToThrowingFood > 2)
            {
                Console.WriteLine("The list of products we threw away:"); ;
                foreach (Item item in allItemsCanDelet)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else
            {
                Console.WriteLine("Sorry, you cannot shop today");
            }
                
            }

         }
    }



