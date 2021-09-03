using System;
using System.Collections.Generic;
using System.Text;

using AnswerKing.Models;

namespace AnswerKing.Repository
{
    interface IOrderLineRepository
    {
        List<OrderLine> GetOrderLines();

        OrderLine GetOrderLineId(int id);

        OrderLine GetOrderLineName(string name);

        OrderLine AddOrderLine(OrderLine OrderLine);

        OrderLine UpdateOrderLine(OrderLine OrderLine);

        void DeleteOrderLine(int id);
    }
}
