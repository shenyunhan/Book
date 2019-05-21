using Book.Data.Entities;
using System;

namespace Book.Models
{
    public class SellModel
    {
        /// <summary>
        /// 图书/售卖ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 图书名称。
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// 剩余数量。
        /// </summary>
        public int Remaining { get; set; }

        /// <summary>
        /// 图书类型。
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 图片路径。
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 出版社。
        /// </summary>
        public string Press { get; set; }

        /// <summary>
        /// 作者。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 出版时间。
        /// </summary>
        public DateTime PulishedDate { get; set; }

        /// <summary>
        /// 图书折旧程度。
        /// </summary>
        public int Depreciation { get; set; }

        /// <summary>
        /// 国际标准图书编号。
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// 价格。
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 图书描述。
        /// </summary>
        public string Description { get; set; }

        public SellModel(BookEntity sell)
        {
            Id = sell.Id;
            BookName = sell.Name;
            Remaining = sell.Remaining;
            Category = sell.Category;
            ImagePath = sell.ImageURL;
            Press = sell.Press;
            Author = sell.Author;
            PulishedDate = sell.PublishedDate;
            Depreciation = sell.Depreciation;
            ISBN = sell.ISBN;
            Price = sell.Price;
            Description = sell.Description;
        }
    }
}
