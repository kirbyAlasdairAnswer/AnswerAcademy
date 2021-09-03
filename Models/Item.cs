using System;
using System.Collections.Generic;
using System.Text;

namespace AnswerKing.Models
{
    public class Item
    {
        public Item(int id, string name, double price, List<Category> categories, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Categories = categories;
            Stock = stock;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<Category> Categories { get; set; }
        public int Stock { get; set; }
    }
}
