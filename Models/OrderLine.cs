using System;
using System.Collections.Generic;
using System.Text;

namespace AnswerKing.Models
{
    public class OrderLine
    {
        public OrderLine(Item item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        public Item Item { get; set; }
        public int Amount { get; set; }
        public double Price
        {
            get
            {
                return Amount * Item.Price;
            }
        }    
    }
}
