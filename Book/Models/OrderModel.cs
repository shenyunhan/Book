using Book.Data.Entities;
using System;

namespace Book.Models
{
    public class OrderModel
    {
        /// <summary>
        /// 图书ID。
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// 买家姓名。
        /// </summary>
        public string BuyerName { get; set; }

        /// <summary>
        /// 买家手机号。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 买家收货地址。
        /// </summary>
        public string Address { get; set; }

        public DateTime TimeStamp { get; set; }

        public OrderModel(OrderEntity order)
        {
            BookId = order.BookId;
            BuyerName = order.BuyerName;
            PhoneNumber = order.PhoneNumber;
            Address = order.Address;
            TimeStamp = order.TimeStamp;
        }
    }
}
