using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml.Linq;
using static Refrigerator.Item;

namespace Refrigerator
{
    internal class Program
    {
        public static void printsContentsRefrigerator(List<Refrigerator> allfrige)
        {
            foreach (Refrigerator friger in allfrige)
            {
                Console.WriteLine(friger.ToString()); 
                foreach (Shelf shelf in friger._shelfes)
                {
                    Console.WriteLine(shelf.ToString());
                    foreach (Item item in shelf._itemsOnShelf)
                    {
                        Console.WriteLine("  "+item.ToString()); 
                    }
                }

            }
        }
        public static Item.Kashrot checkKosherInput()
        {

            int kashrot; ;
            while (true)
            {
                Console.WriteLine("Please select the kosher of the item:");
                Console.WriteLine("1. " + (Item.Kashrot)0);
                Console.WriteLine("2. " + (Item.Kashrot)1);
                Console.WriteLine("3. " + (Item.Kashrot)2);
                string input = Console.ReadLine();
                if (int.TryParse(input, out kashrot) && kashrot > 0 && kashrot < 4)
                {
                    return (Item.Kashrot)(--kashrot);

                }
                else { Console.WriteLine("Please,enter valid input"); }
            }
        }
        public static Item.TypeItem checkTypeItemInput()
        {
            int typeItem;
            while (true)
            {
                Console.WriteLine("lease select the Type of the item:");
                Console.WriteLine("1. " + (Item.TypeItem)0);
                Console.WriteLine("2. " + (Item.TypeItem)1);
                string inp = Console.ReadLine();
                if (int.TryParse(inp, out typeItem) && typeItem > 0 && typeItem < 3)
                {
                    return (Item.TypeItem)(--typeItem);
                }
                else { Console.WriteLine("Please,enter valid input"); }
            }
        }
        public static String checkNameItemInput()
        {
            string nameInput = "";
            Console.WriteLine("Name of the Item");
            nameInput = Console.ReadLine();
            while (nameInput.Length <= 0 || nameInput.Length > 50)
                {
                    Console.WriteLine("Please,enter valid input");
                    nameInput = Console.ReadLine();
                }
            return nameInput;

            
        }
        public static DateTime checkExpiryDatedInput()
        {
            string expiryDateInput;
            DateTime expiryDate = DateTime.Now;
            Console.WriteLine("Please enter the expiry date of the item in this format (MM/dd/yyyy):");
            Console.WriteLine("Dont enter a date before the year 2000");
            expiryDateInput = Console.ReadLine();
            while ((!(DateTime.TryParseExact(expiryDateInput, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expiryDate))) || expiryDate.Year < 2000)
            {
                Console.WriteLine("Please,enter valid input ");
                expiryDateInput = Console.ReadLine();

            }
            return expiryDate;
        }
        public static int checkSizeItemInput()
        {
            string sizeItemInput;
            int sizeItem;
            Console.WriteLine("Please enter the size of thie item");
            Console.WriteLine("Dont insert an item larger than 1000 ");
            sizeItemInput = Console.ReadLine();
            while ((!int.TryParse(sizeItemInput, out sizeItem)) || sizeItem <= 0 || sizeItem > 1000)
            {
                Console.WriteLine("Please,enter valid input ");
                sizeItemInput = Console.ReadLine();

            }
            return sizeItem;
        }           
        static void Main(string[] args)
        {
             

            #region
            List<Item> allItems = new List<Item>();
            allItems.Add(new Item("shkolad", Item.Kashrot.Dairy, Item.TypeItem.Food, new DateTime(2023, 10, 21), 2));
            allItems.Add(new Item("vafle", Item.Kashrot.Fur, Item.TypeItem.Food, new DateTime(2023, 10, 20), 1));
            allItems.Add(new Item("meat", Item.Kashrot.Meat, Item.TypeItem.Drink, new DateTime(2023, 10, 25), 2));
            allItems.Add(new Item("Tamer", Item.Kashrot.Fur, Item.TypeItem.Drink, new DateTime(2021, 10, 19), 1));
            allItems.Add(new Item("Milk", Item.Kashrot.Dairy, Item.TypeItem.Food, new DateTime(2000, 10, 19), 1));

            List<Item> items1 = new List<Item> { allItems[0], allItems[1] , allItems[2] };
            List<Item> items2 = new List<Item> { allItems[3] , allItems[4] };
            List<Item> items3= new List<Item> { };

            List<Shelf> allShelfes = new List<Shelf>();
            allShelfes.Add(new Shelf(1, 40,items1));
            allShelfes.Add(new Shelf(2, 2,items2));
            allShelfes.Add(new Shelf(3, 1,items3));
            allShelfes.Add(new Shelf(4, 1,items3));
            //update shelf of all item that insert to friger
            allItems[0]._shelfOfItem= allShelfes[0];
            allItems[1]._shelfOfItem = allShelfes[0];
            allItems[2]._shelfOfItem = allShelfes[0];
            allItems[3]._shelfOfItem = allShelfes[1];
            allItems[4]._shelfOfItem = allShelfes[1];

            List<Shelf> t = new List<Shelf>();
            t.Add(new Shelf(3, 20, items3));
            t.Add(new Shelf(4, 40, items3));

            List<Refrigerator> allfrige = new List<Refrigerator>();
            allfrige.Add(new Refrigerator(Refrigerator.Model.LG_GR_X265, Refrigerator.Colors.Aquamarine, 4, allShelfes));
            allfrige.Add(new Refrigerator(Refrigerator.Model.LG_GR_X265, Refrigerator.Colors.Aquamarine, 4, t));
            #endregion

            bool exit = false;
            Console.WriteLine("Hello, you have arrived at the program \"The Refrigerator\"\r\nYou can choose one of the options");
            while (!exit)
            {
                Console.WriteLine("1. Printing the refrigerator and all its contents");
                Console.WriteLine("2. Printing the space left in the fridge");
                Console.WriteLine("3. Put an item in the fridge");
                Console.WriteLine("4. Take an item out of the fridge");
                Console.WriteLine("5. Throw away all expired products from the refrigerator");
                Console.WriteLine("6. Please select the type of food and kosher of the item you wish to eat");
                Console.WriteLine("7. Receiving all products sorted by expiration date");
                Console.WriteLine("8. Getting all the shelves sorted according to the free space left on them");
                Console.WriteLine("9. Getting all the refrigerators sorted according to the free space left on them");
                Console.WriteLine("10. Preparing the fridge for shopping");
                Console.WriteLine("100. Exit");
                Console.Write("Please select an option: ");
                Console.WriteLine("");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        printsContentsRefrigerator(allfrige);
                        break;
                    case "2":
                        Console.WriteLine("Free space in the fridge: "+allfrige[0].freeSpaceInRefrigerator());
                        break;
                    case "3":
                        Console.WriteLine("To put item in the fridge, please fill in the following fields");
                        string nameItem = checkNameItemInput();
                        Item.Kashrot kasrotItem = checkKosherInput();
                        Item.TypeItem typeItem = checkTypeItemInput();
                        DateTime expiriDate = checkExpiryDatedInput();
                        int sizeItem = checkSizeItemInput();
                        Item userItem = new Item(nameItem,kasrotItem, typeItem, expiriDate, sizeItem);
                        allItems.Add(userItem);
                        allfrige[0].insertItemToFriger(allItems[(allItems.Count)-1]);

                        break;
                    case "4":
                        int number;
                        while (true)
                        {
                            Console.WriteLine("Please enter Id of the item: ");
                            string input = Console.ReadLine();

                            if (int.TryParse(input, out number) && number > 0)
                            {
                                Console.WriteLine(allfrige[0].takeOffItemFromFriger(number));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a positive integer.");
                            }
                        }
                        break;
                    case "5":
                        allfrige[0].deletExpiredItems(allItems); 
                        break;
                    case "6":
                        Console.WriteLine("Please choose the food you want to eat:");
                        Item.Kashrot kashrot = checkKosherInput();
                        Item.TypeItem type = checkTypeItemInput();
                        List<Item> l = allfrige[0].getYourFoods(kashrot, type);
                        if (l.Count == 0) { Console.WriteLine("Sorry, there is no item like this"); }
                        else
                        {
                            Console.WriteLine("These are the foods you asked for:");
                            foreach (Item item in l)
                            {
                                Console.WriteLine(item.ToString());
                            }

                        }
                        break;
                    case "7":
                        Console.WriteLine("The sorted list of items:");
                        allItems = Item.getSortedItems(allItems);
                        foreach (Item item in allItems)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case "8":
                        Console.WriteLine("The sorted list of Shelfes:");
                        allShelfes = allfrige[0].sortShelfesInFrige();
                        foreach (Shelf shelf in allShelfes)
                        {
                            Console.WriteLine(shelf.ToString());
                        }

                        break;
                    case "9":
                        Console.WriteLine("The sorted list of Refrigerators:");
                        allfrige = Refrigerator.sortRefriger(allfrige);
                        foreach (Refrigerator item in allfrige)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case "10":
                        Console.WriteLine("Preparing the fridge to shopping:");
                        allfrige[0].getReadyForShopping(allItems);
                        break;
                    case "100":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Please enter valid input.");
                        break;
                }
                Console.WriteLine("");
            }

            Console.WriteLine("Goodbye!");

        }
    }
}