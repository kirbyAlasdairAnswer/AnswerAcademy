using System;
using System.Collections.Generic;
using System.Text;
using AnswerKing.Models;

namespace AnswerKing.Services
{
    public interface ICategoryService
    {
        public List<Category> GetCategories();

        public Category GetCategoryId(int id);

        public Category GetCategoryName(string name);

        public Category AddCategory();

        public void ModifyCategory();

    }
}
