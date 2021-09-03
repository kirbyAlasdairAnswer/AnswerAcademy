using System;
using System.Collections.Generic;
using System.Text;

namespace AnswerKing.Models
{
    public class Order
    {
        public Order(int id)
        {
            Id = id;
            OrderLines = new List<OrderLine>();
        }

        public int Id { get; set; }
        public List<OrderLine> OrderLines { get; set; }

        public double Total
        {
            get
            {
                double tally = 0;
                foreach (OrderLine oLine in OrderLines)
                {
                    tally += oLine.Price;
                }
                return tally;
            }
        }

        public void addLine(OrderLine line)
        {
            OrderLines.Add(line);            
        }
    }
}
