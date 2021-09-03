using System;
using System.Collections.Generic;
using System.Text;

namespace AnswerKing.Models
{
    public class Item
    {
        public Item(int id, string name, decimal price, List<Category> categories, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Categories = categories;
            Stock = stock;
            Pic = "";
        }

        public Item(int id, string name, decimal price, List<Category> categories, int stock, string pic)
        {
            Id = id;
            Name = name;
            Price = price;
            Categories = categories;
            Stock = stock;
            Pic = pic;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Category> Categories { get; set; }
        public int Stock { get; set; }

        public string Pic { get; set; }
    }
}
