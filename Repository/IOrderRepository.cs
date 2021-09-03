using System;
using System.Collections.Generic;
using System.Text;
using AnswerKing.Models;

namespace AnswerKing.Repository
{
    interface IOrderRepository
    {
        List<Order> GetOrders();

        Order GetOrderId(int id);

        Order GetOrderName(string name);

        Order AddOrder(Order Order);

        Order UpdateOrder(Order Order);

        void DeleteOrder(int id);
    }
}
