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
                        Console.WriteLine("  " + item.ToString());
                    }
                }

            }
        }
        public static void CheckingIntegrityInput(int userInput)
        {
            if (userInput == -1)
            {
                throw new Exception("Invalid input");
            }

        }
        public static int GetKosherInput()
        {
            int kashrot;
            string userInput;
            Console.WriteLine("Pease select the Kashrot of the item:");
            Console.WriteLine("1. " + (Item.Kashrot)0);
            Console.WriteLine("2. " + (Item.Kashrot)1);
            Console.WriteLine("3. " + (Item.Kashrot)2);
            userInput = Console.ReadLine();
            if (!(int.TryParse(userInput, out kashrot)) || kashrot < 1 || kashrot > 3)
                return -1;
            return --kashrot;

        }
        public static int GetTypeItemInput()
        {
            int typeItem;
            string userInput;
            Console.WriteLine("Pease select the Type of the item:");
            Console.WriteLine("1. " + (Item.TypeItem)0);
            Console.WriteLine("2. " + (Item.TypeItem)1);
            userInput = Console.ReadLine();
            if (!(int.TryParse(userInput, out typeItem)) || typeItem < 1 || typeItem > 2)
                return -1;
            return --typeItem;
        }
        public static String GetNameItemInput()
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
        public static object GetExpiryDatedInput()
        {
            string expiryDateInput;
            DateTime expiryDate = DateTime.Now;
            Console.WriteLine("Please enter the expiry date of the item in this format (MM/dd/yyyy):");
            Console.WriteLine("Dont enter a date before the year 2000");
            expiryDateInput = Console.ReadLine();
            if ((!(DateTime.TryParseExact(expiryDateInput, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expiryDate))) || expiryDate.Year < 2000)
            {
                return -1;

            }
            return expiryDate;
        }
        public static int GetSizeItemInput()
        {
            string sizeItemInput;
            int sizeItem;
            Console.WriteLine("Please enter the size of thie item");
            Console.WriteLine("Dont insert an item larger than 1000 ");
            sizeItemInput = Console.ReadLine();
            if ((!int.TryParse(sizeItemInput, out sizeItem)) || sizeItem <= 0 || sizeItem > 1000)
            {
                return -1;

            }
            return sizeItem;
        }
        public static void CreatDate(List<Item> allItems, List<Shelf> allIShelfes, List<Refrigerator> allIFriger)
        {
            #region
            allItems.Add(new Item("Egges", Item.Kashrot.Dairy, Item.TypeItem.Food, new DateTime(2023, 10, 26), 1));
            allItems.Add(new Item("Milk", Item.Kashrot.Dairy, Item.TypeItem.Drink, new DateTime(2023, 10, 12), 3));
            allItems.Add(new Item("Choklet", Item.Kashrot.Dairy, Item.TypeItem.Food, new DateTime(2023, 10, 27), 3));
            allItems.Add(new Item("Meat", Item.Kashrot.Meat, Item.TypeItem.Food, new DateTime(2023, 10, 30), 5));
            allItems.Add(new Item("Soda", Item.Kashrot.Fur, Item.TypeItem.Drink, new DateTime(2023, 10, 26), 2));
            allItems.Add(new Item("Chees", Item.Kashrot.Dairy, Item.TypeItem.Food, new DateTime(2023, 10, 19), 3));

            List<Item> items1 = new List<Item> { allItems[0], allItems[1], allItems[2] };
            List<Item> items2 = new List<Item> { allItems[3], allItems[4] };
            List<Item> items3 = new List<Item> { allItems[5] };

            allIShelfes.Add(new Shelf(1, 8, items1));
            allIShelfes.Add(new Shelf(2, 7, items2));
            allIShelfes.Add(new Shelf(3, 3, items3));
            //update shelf of all item that insert to friger
            allItems[0]._idShelf = allIShelfes[0].getId();
            allItems[1]._idShelf = allIShelfes[0].getId();
            allItems[2]._idShelf = allIShelfes[0].getId();
            allItems[3]._idShelf = allIShelfes[1].getId();
            allItems[4]._idShelf = allIShelfes[1].getId();
            allItems[5]._idShelf = allIShelfes[2].getId();
            allIFriger.Add(new Refrigerator(Refrigerator.Model.LG_GR_X265, Refrigerator.Colors.Aquamarine, 4, allIShelfes));
            #endregion
        }
        public static string ShowMenu()
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
            Console.WriteLine("");
            string choice = Console.ReadLine();
            return choice;
        }
        public static void ShowFunctionMenu(int numFunction, List<Item> allItems, List<Shelf> allIShelfes, List<Refrigerator> allIFriger)
        {
            switch (numFunction)
            {
                case 1:
                    printsContentsRefrigerator(allIFriger);
                    break;
                case 2:
                    Console.WriteLine("Free space in the fridge: " + allIFriger[0].freeSpaceInRefrigerator());
                    break;
                case 3:
                    try
                    {
                        Console.WriteLine("To put item in the fridge, please fill in the following fields");
                        string nameItem = GetNameItemInput();
                        int userTypeItem = GetTypeItemInput();
                        CheckingIntegrityInput(userTypeItem);
                        Item.TypeItem typeItem = (Item.TypeItem)(userTypeItem);
                        int userKosher = GetKosherInput();
                        CheckingIntegrityInput(userKosher);
                        Item.Kashrot kasrotItem = (Item.Kashrot)(userKosher);
                        object userExpiryDated = GetExpiryDatedInput();
                        if (userExpiryDated is int)
                        {
                            throw new Exception("Invalid input");
                        }
                        DateTime expiriDate = (DateTime)userExpiryDated;
                        int userSizeItem = GetSizeItemInput();
                        CheckingIntegrityInput(userSizeItem);
                        Item userItem = new Item(nameItem, kasrotItem, typeItem, expiriDate, userSizeItem);
                        allItems.Add(userItem);
                        allIFriger[0].insertItemToFriger(allItems[(allItems.Count) - 1]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 4:
                    int number;
                    Console.WriteLine("Please enter Id of the item: ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out number) && number > 0)
                    {
                        Console.WriteLine(allIFriger[0].takeOffItemFromFriger(number));
                        return;
                    }
                    Console.WriteLine("Invalid input. Please enter a positive integer.");
                    break;
                case 5:
                    allIFriger[0].deletExpiredItems(allItems);
                    break;
                case 6:
                    try
                    {
                        Console.WriteLine("Please choose the food you want to eat:");
                        int uesrKashrot = GetKosherInput();
                        CheckingIntegrityInput(uesrKashrot);
                        Item.Kashrot kashrot = (Item.Kashrot)(uesrKashrot);
                        int userTypeItem = GetTypeItemInput();
                        CheckingIntegrityInput(userTypeItem);
                        Item.TypeItem type = (Item.TypeItem)(userTypeItem);
                        List<Item> l = allIFriger[0].getYourFoods(kashrot, type);
                        if (l.Count == 0)
                        {
                            Console.WriteLine("Sorry, there is no item like this");
                            break;
                        }
                        Console.WriteLine("These are the foods you asked for:");
                        foreach (Item item in l)
                        {
                            Console.WriteLine(item.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 7:
                    Console.WriteLine("The sorted list of items:");
                    allItems = Item.getSortedItems(allItems);
                    foreach (Item item in allItems)
                    {
                        Console.WriteLine(item.ToString());
                    }
                    break;
                case 8:
                    Console.WriteLine("The sorted list of Shelfes:");
                    allIShelfes = allIFriger[0].sortShelfesInFrige();
                    foreach (Shelf shelf in allIShelfes)
                    {
                        Console.WriteLine(shelf.ToString());
                    }

                    break;
                case 9:
                    Console.WriteLine("The sorted list of Refrigerators:");
                    allIFriger = Refrigerator.sortRefriger(allIFriger);
                    foreach (Refrigerator item in allIFriger)
                    {
                        Console.WriteLine(item.ToString());
                    }
                    break;
                case 10:
                    Console.WriteLine("Preparing the fridge to shopping:");
                    allIFriger[0].getReadyForShopping(allItems);
                    break;
            }
        }
        static void Main(string[] args)
        {
            List<Item> allItems = new List<Item>();
            List<Shelf> allIShelfes = new List<Shelf>();
            List<Refrigerator> allIFriger = new List<Refrigerator>();
            CreatDate(allItems, allIShelfes, allIFriger);
            Console.WriteLine("Hello, you have arrived at the program \"The Refrigerator\"\r\nYou can choose one of the options");
            int choice;
            do
            {
                string userchoice = ShowMenu();
                if ((!(int.TryParse(userchoice, out choice)) || choice < 1 || choice > 10) && choice != 100)
                {
                    Console.WriteLine("Invalid input");
                    return;
                }
                if (choice != 100)
                {
                    ShowFunctionMenu(choice, allItems, allIShelfes, allIFriger);
                }
                Console.WriteLine("");
            } while (choice != 100);
            Console.WriteLine("Goodbye!");
            return;
        }
    }
}

