using System;
using System.Collections.Generic;
using System.Text;
using AnswerKing.Models;

namespace AnswerKing.Services
{
    public interface IItemService
    {
        IEnumerable<Item> getItems();

        Item getItemId(int id);

        Item getItemName(string name);

        IEnumerable<Item> getItemCategory(Category category);

        void addItem();

        void modifyItem();

        bool modifyItemStock(Item item, int change);

        void removeItem();

        void removeItemCategory(Category category);

        bool purchaseItem(int id, int number);
    }
}
