using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevLavka.Models;
using DevLavka.Models.EntityFramework.Repositories;
using DevLavka.Models.Frontend.ArgsModels.ArticleController;
using DevLavka.Models.Frontend.ResponseModels;
using DevLavka.Models.Frontend.ResponseModels.ArticleController;
using Microsoft.AspNetCore.Mvc;

namespace DevLavka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private IBaseRepository<Article> _articleRepository;

        public ArticleController(IBaseRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet("id{articleId}")]
        public async Task<GetArticleResponse> GetArticle(int articleId)
        {
            Article? article = null;

            await Task.Run(() =>
            {
                try
                {
                    article = _articleRepository.GetAll().Single(a => a.Id == articleId);
                }
                catch
                {
                    article = null;
                }
            });

            if (article == null)
                return new GetArticleResponse("Article with this id don't found", 500);

            return new GetArticleResponse(article, 200);
        }
        [HttpPost("create")]
        public async Task<TextServerResponse> CreateArticle(ArticleModel articleModel)
        {
            Article article = new Article(
                articleModel.Title, articleModel.ProductName,
                articleModel.Description, articleModel.DownloadLink,
                articleModel.Author, articleModel.Specialization
            );

            await _articleRepository.CreateAsync(article);

            return new TextServerResponse("Successfully create article", 200);
        }
        [HttpDelete("delete")]
        public async Task<TextServerResponse> DeleteArticle(int id)
        {
            Article? deleteArticle = null;

            try
            {
                deleteArticle = _articleRepository.GetAll().Single(a => a.Id == id);
            }
            catch
            {
                return new TextServerResponse("Article with this id don't found", 500);
            }

            await _articleRepository.DeleteAsync(deleteArticle);

            return new TextServerResponse("Successfully delete article", 200);
        }
        [HttpPut("update")]
        public async Task<TextServerResponse> UpdateArticle(UpdateArticleModel updatedArticleModel)
        {
            ArticleModel updatedArticle = updatedArticleModel.NewArticle;

            Article newArticle = new Article(
                updatedArticle.Title, updatedArticle.ProductName,
                updatedArticle.Description, updatedArticle.DownloadLink,
                updatedArticle.Author, updatedArticle.Specialization
            );

            newArticle.Id = updatedArticleModel.OldArticleId;

            try
            {
                await _articleRepository.UpdateAsync(newArticle);
            }
            catch
            {
                return new TextServerResponse("Article with this id don't found", 500);
            }

            return new TextServerResponse("Successfully update article", 200);
        }
    }
}