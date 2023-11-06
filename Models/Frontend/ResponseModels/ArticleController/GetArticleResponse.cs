using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLavka.Models.Frontend.ResponseModels.ArticleController
{
    public class GetArticleResponse
    {
        public GetArticleResponse(Article article, int statusCode)
        {
            Article = article;
            Description = null;
            StatusCode = statusCode;
        }

        public GetArticleResponse(string description, int statusCode)
        {
            Article = null;
            Description = description;
            StatusCode = statusCode;
        }

        public Article? Article { get; set; }
        public string? Description { get; set; }
        public int StatusCode { get; set; }
    }
}