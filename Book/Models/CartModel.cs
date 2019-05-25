using Book.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class CartModel
    {
        /// <summary>
        /// 图书ID。
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// 图书名称。
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// 出版社。
        /// </summary>
        public string Press { get; set; }

        /// <summary>
        /// 作者。
        /// </summary>
        public string Author { get; set; }

        public int Number { get; set; }

        public double Price { get; set; }

        /// <summary>
        /// 图片路径。
        /// </summary>
        public string ImagePath { get; set; }

        public CartModel(BookEntity book)
        {
            BookId = book.Id;
            BookName = book.Name;
            Press = book.Press;
            Author = book.Author;
            Price = book.Price;
            ImagePath = book.ImageURL;
        }
    }
}
