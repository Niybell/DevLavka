using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLavka.Models.Frontend.ArgsModels.ArticleController
{
    public class UpdateArticleModel
    {
        public UpdateArticleModel(int oldArticleId, ArticleModel newArticle)
        {
            OldArticleId = oldArticleId;
            NewArticle = newArticle;
        }

        public int OldArticleId { get; set; }
        public ArticleModel NewArticle { get; set; }
    }
}