using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLavka.Models.Frontend.ResponseModels.ArticleController
{
    public class GetArticlesResponse
    {
        public GetArticlesResponse(Article[]? articles, int statusCode, string? description, int pagesCount)
        {
            Articles = articles;
            StatusCode = statusCode;
            Description = description;
            PagesCount = pagesCount;
        }

        public Article[]? Articles { get; set; }
        public int StatusCode { get; set; }
        public string? Description { get; set; }
        public int PagesCount { get; set; }
        public int TotalCount => Articles == null ? 0 : Articles.Length;
    }
}