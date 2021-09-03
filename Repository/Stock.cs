using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AnswerKing.Models;

namespace AnswerKing.Repository
{
    public class Stock
    {
        public Stock()
        {
            items = new List<Item>();
            categories = new List<Category>();

            InitialStock();
        }

        public List<Item> items { get; set; }
        public List<Category> categories { get; set; }

        public void InitialStock()
        {
            Category saver = new Category(1, "Saver");
            Category special = new Category(2, "Specials");
            Category drinks = new Category(3, "Drinks");
            Category puddings = new Category(4, "Desserts");
            Category summer = new Category(5, "Summer Special");

            categories.Add(saver);
            categories.Add(special);
            categories.Add(drinks);
            categories.Add(puddings);
            categories.Add(summer);

            Item burger = new Item(1, "Burgers", 1.5m, new List<Category> { saver }, 2);
            Item fries = new Item(2, "Fries", 1.0m, new List<Category> { saver }, 4);
            Item coke = new Item(3, "Coke's", 1.0m, new List<Category> { drinks}, 4);
            Item cake = new Item(4, "Cakes", 1.0m, new List<Category> { puddings }, 4);
            Item bbqburger = new Item(5, "BBQ Burgers", 2, new List<Category> { special }, 1);
            Item nuggets = new Item(6, "Nuggets", 3.0m, new List<Category>() , 4);


            items.Add(burger);
            items.Add(fries);
            items.Add(cake);
            items.Add(coke);
            items.Add(bbqburger);
            items.Add(nuggets);

        }

        public List<Category> GetCategories()
        {
            HashSet<Category> categories = new HashSet<Category>();

            foreach(Item item in items)
            {
                foreach(Category category in item.Categories)
                {
                    categories.Add(category);
                }                
            }

            return categories.ToList();
        }

        public int GetNewItemId()
        {
            return items.Last().Id + 1;
        }

        public int GetNewCategoryId()
        {
            return categories.Last().Id + 1;
        }
    }
}
