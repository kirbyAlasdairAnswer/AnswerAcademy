using System;
using System.Linq;

using AnswerKing.Models;
using AnswerKing.Services;
using AnswerKing.Repository;

namespace AnswerKing
{
    class Program
    {
        static Stock stock = new Stock();

        static private IItemService _itemService;
        static private ICategoryService _categoryService;

        static private Order _order;
        static Art _art;

        static void Main(string[] args)
        {
            _categoryService = new CategoryService(stock, _itemService);
            _itemService = new ItemService(stock, _categoryService);
            _art = new Art();

            WelcomeInterface();
        }

        static void WelcomeInterface()
        {
            Console.WriteLine("");
            Console.WriteLine("    Welcome to the AnswerKing service are you a Customer or Admin?");

            bool transactionRepeat = true;
            while (transactionRepeat)
            {
                Console.WriteLine("    Please Enter Customer or Admin:");
                string transaction = Console.ReadLine();
                if (transaction == "Customer")
                {
                    transactionRepeat = false;
                    _order = new Order(1);
                    CustomerInterface();
                }
                else if (transaction == "Admin")
                {
                    transactionRepeat = false;
                    AdminInterface();
                }
                else
                {
                    Console.WriteLine("    Sorry, that wasnt recognised");
                }
            }
        }
        static void CustomerInterface()
        {
            bool customerChoice = true;
            while (customerChoice)
            {
                Console.WriteLine(_art.burger);

                Console.WriteLine("    Welcome to the shop, please choose an option: ");
                Console.WriteLine("    ");
                Console.WriteLine("    1. Browse items by Category");
                Console.WriteLine("    2. Browse all items");
                Console.WriteLine("");
                Console.WriteLine("    3. View your order");
                Console.WriteLine("    4. Back");
                Console.WriteLine("    5. Exit");
                Console.WriteLine("");

                while (customerChoice)
                {
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            CategoryInterface();
                            break;
                        case 2:
                            ItemInterface();
                            break;
                        case 3:
                            OrderInterface();
                            break;
                        case 4:
                            EmptyCart();
                            WelcomeInterface();
                            break;
                        case 5:
                            EmptyCart();
                            ExitInterface();
                            break;
                        default:
                            Console.WriteLine("    Sorry that option was not valid, please try again");
                            Console.WriteLine("");
                            break;
                    }
                }
                
            }
        }

        static void CategoryInterface()
        {
            bool customerChoice = true;
            while (customerChoice)
            {                
                Console.WriteLine("    This is the category selection screen, please choose a selection by number");
                Console.WriteLine("");

                int catCount = 1;
                foreach (Category category in stock.GetCategories())
                {
                    Console.WriteLine($"    {catCount}. The {category.Name} Category");
                    catCount++;
                }
                Console.WriteLine("");
                Console.WriteLine($"    {catCount}. Back");
                Console.WriteLine("");

                try
                {
                    int categoryChoice = int.Parse(Console.ReadLine());

                    if (categoryChoice == catCount)
                    {
                        CustomerInterface();
                    }
                    else if (categoryChoice < catCount && categoryChoice > 0)
                    {
                        AddItemInterface(stock.GetCategories()[categoryChoice-1]);
                    }
                } catch {
                    Console.WriteLine("");
                    Console.WriteLine("    Sorry, that wasnt recognised");
                }              
            }            
        }

        static void ItemInterface()
        {
            bool customerChoice = true;
            while (customerChoice)
            {
                Console.WriteLine("");
                Console.WriteLine($"    The menu contains:");

                foreach (Item item in stock.items)
                {
                    Console.WriteLine($"    {item.Name} which cost " + item.Price.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-gb")));
                }
                Console.WriteLine("    Back");

                Console.WriteLine("");
                Console.WriteLine("    Which item is requested?");
                string choice = Console.ReadLine();
                if (choice == "Back") CategoryInterface();
                Item chosen = _itemService.getItemName(choice);
                if (chosen == null)
                {
                    Console.WriteLine("    Sorry, that was not a valid selection");
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("    And how many?");
                    int amount = int.Parse(Console.ReadLine());

                    if (_itemService.purchaseItem(chosen.Id, amount))
                    {
                        var orderLine = new OrderLine(chosen, amount);
                        Console.WriteLine($"    {amount} {choice} have been added to your order");
                        _order.addLine(orderLine);
                        Console.WriteLine($"    Your total is {_order.Total.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-gb"))}");
                    }
                    else
                    {
                        Console.WriteLine($"    Sorry, there is not enough {chosen.Name} in stock, the maximum order is {chosen.Stock}");
                    }
                }
            }

        }

        static void AddItemInterface(Category category)
        {
            bool customerChoice = true;
            while (customerChoice)
            {
                Console.WriteLine("");
                Console.WriteLine($"    The {category.Name} menu contains:");

                foreach (Item item in _itemService.getItemCategory(category))
                {
                    if (item.Stock == 0)
                    {
                        Console.WriteLine($"    {item.Name} which is OUT OF STOCK");
                    } else
                    {
                        Console.WriteLine($"    {item.Name} which cost " + item.Price.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-gb")));
                    }
                }
                Console.WriteLine("    Back");

                Console.WriteLine("");
                Console.WriteLine("    Which item is requested?");
                string choice = Console.ReadLine();
                if (choice == "Back") CategoryInterface();
                Item chosen = _itemService.getItemName(choice);
                if (chosen == null)
                {
                    Console.WriteLine("    Sorry, that was not a valid selection");
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("    And how many?");
                    try
                    {
                        int amount = int.Parse(Console.ReadLine());

                        if (_itemService.purchaseItem(chosen.Id, amount))
                        {
                            var exists = _order.OrderLines.FirstOrDefault(line => line.Item.Name == choice);
                            if (exists != null)
                            {
                                exists.Amount += amount;
                                Console.WriteLine($"    {amount} {choice} have been added to your order");
                                Console.WriteLine($"    Your total is {_order.Total.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-gb"))}");
                            }

                            var orderLine = new OrderLine(chosen, amount);
                            Console.WriteLine($"    {amount} {choice} have been added to your order");
                            _order.addLine(orderLine);
                            Console.WriteLine($"    Your total is {_order.Total.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-gb"))}");
                        }
                        else
                        {
                            Console.WriteLine($"    Sorry, there is not enough {chosen.Name} in stock, the maximum order is {chosen.Stock}");
                        }
                    } catch
                    {
                        Console.WriteLine("    Sorry, that was not a valid selection");
                    }                    
                }
            }
        }

        static void OrderInterface()
        {
            bool customerChoice = true;
            while (customerChoice)
            {
                Console.WriteLine("    Your Order is:");
                Console.WriteLine("");
                Console.WriteLine("    _________________");

                foreach (OrderLine line in _order.OrderLines)
                {
                    Console.WriteLine($"    {line.Item.Name}   x   {line.Amount}  =  {line.Price.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-gb"))}");
                }

                Console.WriteLine("");
                Console.WriteLine("    _________________");
                Console.WriteLine($"    Your total is {_order.Total.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-gb"))}");

                Console.WriteLine("    _________________");
                Console.WriteLine("");
                Console.WriteLine("    What would you like to do?");
                Console.WriteLine("    1. Add more items");
                Console.WriteLine("    2. Append my order");
                Console.WriteLine("    3. Checkout");
                Console.WriteLine("    4. Exit");
                Console.WriteLine("");

                try
                {
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            CategoryInterface();
                            break;
                        case 2:
                            EditOrderInterface();
                            break;
                        case 3:
                            CheckoutInterface();
                            break;
                        case 4:
                            ExitInterface();
                            break;
                    }
                } catch
                {
                    Console.WriteLine("");
                    Console.WriteLine("    Sorry that was not a valid selection");

                }
            }
        }

        static void EditOrderInterface()
        {
            bool customerChoice = true;
            while (customerChoice)
            {
                Console.WriteLine("");
                Console.WriteLine("    Your Order contains:");
                Console.WriteLine("");
                foreach (OrderLine line in _order.OrderLines)
                {
                    Console.WriteLine($"{line.Item.Name}  x  {line.Amount}");
                }

                Console.WriteLine("    Which item would you like to change? Or select Back");
                string choice = Console.ReadLine();
                if (choice == "Back") OrderInterface();
                Item chosen = _itemService.getItemName(choice);
                if (chosen == null || !_order.OrderLines.Any(line => line.Item.Name == choice))
                {
                    Console.WriteLine("");
                    Console.WriteLine("    Sorry, that was not a valid selection");
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("    By how many would you like to change the order (negative to remove)?");
                    try
                    {
                        var itemChanging = _order.OrderLines.First(line => line.Item.Name == choice);
                        int amount = int.Parse(Console.ReadLine());
                        if (amount <= itemChanging.Amount)
                        {
                            if (_itemService.modifyItemStock(chosen, amount))
                            {
                                itemChanging.Amount += amount;
                                Console.WriteLine("");
                                Console.WriteLine("    Order updated");
                                Console.WriteLine($"    {chosen.Name} has been changed by {amount}");

                                if (itemChanging.Amount == 0) _order.OrderLines.Remove(itemChanging);
                            }
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("    Sorry, You cannot remove that many items");
                        }

                    } catch
                    {
                        Console.WriteLine("");
                        Console.WriteLine("    Sorry, that was not a valid selection");
                    }
                }
            }
        }

        static void CheckoutInterface()
        {
            bool customerChoice = true;
            while (customerChoice)
            {
                Console.WriteLine("");
                Console.WriteLine($"    Your total is {_order.Total.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-gb"))}");
                Console.WriteLine("    Do you wish to checkout? Y/N");

                string confirmation = Console.ReadLine();
                if (confirmation == "Y")
                {
                    Console.WriteLine("");
                    Console.WriteLine("    Thank you for shopping at AnswerKing!");
                    Console.WriteLine("    Enjoy your meal!");
                    ExitInterface();
                } else if (confirmation == "N")
                {
                    Console.WriteLine("");
                    Console.WriteLine("    Do you wish to cancel the order? Y/N");
                    string secondConfirmation = Console.ReadLine();
                    if (confirmation == "Y")
                    {
                        foreach (OrderLine line in _order.OrderLines)
                        {
                            _itemService.modifyItemStock(line.Item, line.Amount);
                        }

                    } else if (secondConfirmation == "N")
                    {
                        OrderInterface();
                    } else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("    Sorry that was not valid");
                    }
                } else
                {
                    Console.WriteLine("");
                    Console.WriteLine("    Sorry that was not valid");
                }

            }
        }

        static void AdminInterface()
        {
            bool customerChoice = true;
            while (customerChoice)
            {
                Console.WriteLine("");
                Console.WriteLine("    Welcome to Admin Interface, please select an option by number");
                Console.WriteLine("");
                Console.WriteLine("    1. Add an Item");
                Console.WriteLine("    2. Modify an Item");
                Console.WriteLine("    3. Remove an Item");
                Console.WriteLine("    4. View Items");
                Console.WriteLine("");
                Console.WriteLine("    5. Add a Category");
                Console.WriteLine("    6. Modify a Category");
                Console.WriteLine("    7. Remove a Category");
                Console.WriteLine("    8. View Categories");
                Console.WriteLine("");
                Console.WriteLine("    9. Add Stock");
                Console.WriteLine("");
                Console.WriteLine("    10. Back");
                Console.WriteLine("    11. Exit");


                try
                {
                    int input = int.Parse(Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            _itemService.addItem();
                            break;
                        case 2:
                            _itemService.modifyItem();
                            break;
                        case 3:
                            _itemService.removeItem();
                            break;
                        case 4:
                            Console.WriteLine("");
                            Console.WriteLine("    Here are the Items in the shop");
                            Console.WriteLine("");
                            foreach (Item item in stock.items)
                            {
                                Console.WriteLine($"    {item.Name} are in stock, there is {item.Stock}");
                            }
                            break;
                        case 5:
                            _categoryService.AddCategory();
                            break;
                        case 6:
                            _categoryService.ModifyCategory();
                            break;
                        case 7:
                            RemoveCategory();
                            break;
                        case 8:
                            Console.WriteLine("");
                            Console.WriteLine("    Here are the Categories currentely in use:");
                            Console.WriteLine("");
                            foreach (Category category in stock.categories)
                            {
                                Console.WriteLine(category.Name);
                            }
                            break;
                        case 9:
                            Console.WriteLine("");
                            Console.WriteLine("    Here are the Items in the shop");
                            Console.WriteLine("");
                            foreach (Item item in stock.items)
                            {
                                Console.WriteLine($"    {item.Name} are in stock, there is {item.Stock}");
                            }
                            Console.WriteLine("Back");

                            Console.WriteLine("");
                            Console.WriteLine("    Which item is requested?");
                            string choice = Console.ReadLine();
                            if (choice == "Back") CategoryInterface();
                            Item chosen = _itemService.getItemName(choice);
                            if (chosen == null)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("    Sorry, that was not a valid selection");
                            }
                            else
                            {
                                Console.WriteLine("");
                                Console.WriteLine("    And change by how much?");
                                try
                                {
                                    int amount = int.Parse(Console.ReadLine());

                                    if (_itemService.modifyItemStock(chosen, amount))
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine($"    There are now {chosen.Stock} {chosen.Name}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine($"    Sorry, there are not enough {chosen.Name} in stock, the maximum order is {chosen.Stock}");
                                    }
                                } catch
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("    Sorry, that is not a valid selction");
                                }
                            }
                            break;
                        case 10:
                            WelcomeInterface();
                            break;
                        case 11:
                            ExitInterface();
                            break;
                        default:
                            Console.WriteLine("");
                            Console.WriteLine("    Sorry that was not a valid selection");
                            break;
                    }
                } catch
                {
                    Console.WriteLine("");
                    Console.WriteLine("    Sorry, that was not valid");
                    AdminInterface();
                }             
            }
        }

        static void ExitInterface()
        {
            //check total
            //add itemsback
            Console.WriteLine("");
            Console.WriteLine("    Thank you for shopping at AnswerKing");
            System.Environment.Exit(1);
        }

        static void RemoveCategory()
        {
            Console.WriteLine("");
            Console.WriteLine("    The current categories are:");

            foreach (Category category in stock.categories)
            {
                Console.WriteLine(category.Name);
            }
            Console.WriteLine("");
            Console.WriteLine("    Please select a category:");
            Console.WriteLine("");

            Category current = _categoryService.GetCategoryName(Console.ReadLine());

            if (current == null) Console.WriteLine("    Invalid choice");
            else
            {
                var items = _itemService.getItemCategory(current);

                Console.WriteLine("");
                Console.WriteLine($"    You wish to delete the {current.Name} from {items.Count()} items");
                Console.WriteLine("    Do you want to proceed? Y/N");
                Console.WriteLine("");

                if (Console.ReadLine() == "Y")
                {
                    _itemService.removeItemCategory(current);
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("    Aborted deletion");
                }
            }            
        }

        static public void EmptyCart()
        {
            foreach (OrderLine line in _order.OrderLines)
            {
                _itemService.modifyItemStock(line.Item, line.Amount);
            }
        }
    }
}