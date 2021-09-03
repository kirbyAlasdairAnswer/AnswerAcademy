using System;
using System.Collections.Generic;
using System.Text;
using AnswerKing.Models;

namespace AnswerKing.Repository
{
    interface ICategory
    {
        List<Category> GetCategories();

        Category GetCategoryId(int id);

        Category GetCategoryName(string name);

        Category AddCategory(Category category);

        Category UpdateCategory(Category category);

        void DeleteCategory(int id);
    }
}
