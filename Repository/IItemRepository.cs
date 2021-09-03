using System;
using System.Collections.Generic;
using System.Text;

using AnswerKing.Models;

namespace AnswerKing.Repository
{
    interface IItemRepository
    {
        List<Item> GetItems();

        Item GetItemId(int id);

        Item GetItemName(string name);

        Item AddItem(Item Item);

        Item UpdateItem(Item Item);

        void DeleteItem(int id);
    }
}
