using AnswerKing.Models;
using System.Collections.Generic;
using System;
using System.Linq;

using AnswerKing.Repository;

namespace AnswerKing.Services
{
    public class ItemService : IItemService
    {
        Stock Stock;

        private readonly ICategoryService _categoryService;

        public ItemService(Stock stock, ICategoryService categoryService)
        {
            Stock = stock;
            _categoryService = categoryService;
        }

        public IEnumerable<Item> getItemCategory(Category category)
        {
            return from item in Stock.items
                   where item.Categories.Contains(category)
                   select item;
        }

        public Item getItemId(int id)
        {
            return Stock.items.FirstOrDefault(item => item.Id == id);
        }

        public Item getItemName(string name)
        {
            return Stock.items.FirstOrDefault(item => item.Name == name);
        }

        public IEnumerable<Item> getItems()
        {
            return Stock.items;
        }

        public void addItem()
        {
            int id = Stock.GetNewItemId();
            Console.WriteLine("What is the Name?");
            string name = Console.ReadLine();
            Console.WriteLine("What is the Price?");
            double price = double.Parse(Console.ReadLine());
            bool cats = true;
            List<Category> categories = new List<Category>();
            while (cats)
            {
                Console.WriteLine("What Categories does it belong to?");
                int count = 1;
                foreach (Category cat in Stock.categories)
                {
                    Console.WriteLine($"{count}. {cat.Name}");
                    count++;
                }
                Console.WriteLine($"{count}. Add a category");
                count++;
                Console.WriteLine($"{count}. None/No more");

                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == count) cats = false;
                    else if (choice == count - 1)
                    {
                        var cat = _categoryService.AddCategory();
                        categories.Add(cat);
                    }
                    else if (choice > 0 && choice < count - 1)
                    {
                        categories.Add(Stock.categories[choice - 1]);
                    }
                } catch
                {
                    Console.WriteLine("Sorry, not a valid choice");
                }               
            }
            Console.WriteLine("How many are in stock?");
            int stock = int.Parse(Console.ReadLine());
            Item item = new Item(id, name, price, categories, stock);
            Stock.items.Add(item);
        }

        public void modifyItem()
        {
            Console.WriteLine("The current items are:");
            foreach(Item existingItem in Stock.items)
            {
                Console.WriteLine(existingItem.Name);
            }
            Console.WriteLine("Please input the item to change");
            Item existing = getItemName(Console.ReadLine());

            Console.WriteLine("You may now change the item, input new value when prompted or hit 'enter' to skip");
            Console.WriteLine("");

            Console.WriteLine($"The current Name is, {existing.Name}");
            Console.WriteLine("What is the new Name?");
            string nameIn = Console.ReadLine();
            if (nameIn != "")
            {
                existing.Name = nameIn;
            }
            Console.WriteLine($"The current Price is, {existing.Price}");
            Console.WriteLine("What is the new Price?");
            string priceIn = Console.ReadLine();
            if (priceIn != "")
            {
                try
                {
                    double price = double.Parse(priceIn);
                    existing.Price = price;
                } catch
                {
                    Console.WriteLine("Invalid Price");
                }

            }
            
            Console.WriteLine($"The current Categories are: ");
            foreach (Category c in existing.Categories)
            {
                Console.WriteLine(c.Name);
            }

            bool cats = true;
            List<Category> categories = new List<Category>();
            while (cats)
            {
                Console.WriteLine("What Categories does it now belong to?");
                int count = 1;
                foreach (Category cat in Stock.categories)
                {
                    Console.WriteLine($"{count}. {cat.Name}");
                    count++;
                }
                Console.WriteLine($"{count}. Add a category");
                count++;
                Console.WriteLine($"{count}. None/No more");

                string catIn = Console.ReadLine();
                if (catIn != "")
                {
                    try
                    {
                        int choice = int.Parse(catIn);
                        if (choice == count) cats = false;
                        else if (choice == count - 1)
                        {
                            categories.Add(_categoryService.AddCategory());
                        }
                        else if (choice > 0 && choice < count - 1)
                        {
                            categories.Add(Stock.categories[choice - 1]);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Sorry, not a valid choice");
                    }
                } else
                {
                    cats = false;
                    categories = existing.Categories;
                }
            }
            existing.Categories = categories;
            Console.WriteLine($"The current Stock is, {existing.Stock}");
            Console.WriteLine("How many are now in stock?");
            string stockIn = Console.ReadLine();
            if (stockIn != "")
            {
                try
                {
                    existing.Stock = int.Parse(stockIn);
                } catch
                {
                    Console.WriteLine("Sorry, this is not a valid input");
                }
            }
        }

        public bool modifyItemStock(Item item, int change)
        {
            if (change > 0)
            {
                item.Stock += change;
                return true;
            } else
            {
                if (item.Stock >= change)
                {
                    item.Stock -= change;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void removeItem()
        {
            Console.WriteLine("The current items are:");
            foreach (Item existingItem in Stock.items)
            {
                Console.WriteLine("");
                Console.WriteLine(existingItem.Name);
                Console.WriteLine("");
            }

            Console.WriteLine("Please input the item to change");
            Item existing = getItemName(Console.ReadLine());

            try
            {
                Stock.items.Remove(existing);
                Console.WriteLine("");
                Console.WriteLine("Item successfully removed");
                Console.WriteLine("");
            }
            catch
            {
                Console.WriteLine("");
                Console.WriteLine("Invalid Item chosen");
                Console.WriteLine("");
            }
        }

        public void removeItemCategory(Category category)
        {
            foreach (Item item in Stock.items)
            {
                item.Categories.Remove(category);
            }
        }

        public bool purchaseItem(int id, int number)
        {
            var item = getItemId(id);

            if ( item.Stock < number)
            {
                return false;
            }
            else
            {
                item.Stock -= number;
                return true;
            }
        }        
    }
}