using Book.Data.Entities;
using System;

namespace Book.Models
{
    public class RewardModel
    {
        /// <summary>
        /// 悬赏ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 悬赏图书名称。
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// 出版社。
        /// </summary>
        public string Press { get; set; }

        /// <summary>
        /// 图书分类。
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 作者。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 国际标准图书编号。
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// 悬赏价格。
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 图片链接。
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 出版时间。
        /// </summary>
        public DateTime PublishedDate { get; set; }

        /// <summary>
        /// 折旧程度。
        /// </summary>
        public int Depreciation { get; set; }

        /// <summary>
        /// 图书描述。
        /// </summary>
        public string Description { get; set; }

        public RewardModel(RewardEntity reward)
        {
            Id = reward.Id;
            BookName = reward.BookName;
            Press = reward.Press;
            Category = reward.Category;
            Author = reward.Author;
            ISBN = reward.ISBN;
            Price = reward.Price;
            ImagePath = reward.ImageURL;
            PublishedDate = reward.PublishedDate;
            Depreciation = reward.Depreciation;
            Description = reward.Description;
        }
    }
}
