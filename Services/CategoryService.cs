using AnswerKing.Models;
using System.Collections.Generic;
using AnswerKing.Repository;
using System;
using System.Linq;

namespace AnswerKing.Services
{
    public class CategoryService : ICategoryService
    {
        Stock Stock;

        private readonly IItemService _itemService;

        public CategoryService(Stock stock, IItemService itemService)
        {
            Stock = stock;
            _itemService = itemService;
        }

        public Category AddCategory()
        {
            try
            {
                int id = Stock.GetNewCategoryId();
                Console.WriteLine("Please give the Name");
                string name = Console.ReadLine();
                Category cat = new Category(id, name);
                Stock.categories.Add(cat);
                return cat;
            } catch
            {
                Console.WriteLine("Invlid input");
                return null;
            }
            
        }
        public List<Category> GetCategories()
        {
            return Stock.categories;
        }
        public Category GetCategoryId(int id)
        {
            return Stock.categories.FirstOrDefault(cat => cat.Id == id);
        }

        public Category GetCategoryName(string name)
        {
            return Stock.categories.FirstOrDefault(cat => cat.Name == name);
        }
        public void ModifyCategory()
        {
            Console.WriteLine("The current categories are:");

            foreach(Category category in Stock.categories)
            {
                Console.WriteLine(category.Name);
            }
            Console.WriteLine("");
            Console.WriteLine("Please select a category:");
            Console.WriteLine("");

            Category current = GetCategoryName(Console.ReadLine());

            try
            {
                Console.WriteLine($"The current Category Name is {current.Name}");
                Console.WriteLine("What is the new name?");

                current.Name = Console.ReadLine();


            } catch
            {
                Console.WriteLine("Invalid new name");
            }
        }
    }
}