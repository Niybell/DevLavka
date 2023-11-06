using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLavka.Models.Frontend.ArgsModels.ArticleController
{
    public class ArticleModel
    {
        public ArticleModel(string title, string productName, string description, string downloadLink, string author, Specialization specialization)
        {
            Title = title;
            ProductName = productName;
            Description = description;
            DownloadLink = downloadLink;
            Author = author;
            Specialization = specialization;
        }

        public string Title { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string DownloadLink { get; set; }
        public string Author { get; set; }
        public Specialization Specialization { get; set; }
    }
}