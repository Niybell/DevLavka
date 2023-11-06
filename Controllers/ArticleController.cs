using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevLavka.Models;
using DevLavka.Models.EntityFramework.Repositories;
using DevLavka.Models.Frontend.ArgsModels.ArticleController;
using DevLavka.Models.Frontend.ResponseModels;
using DevLavka.Models.Frontend.ResponseModels.ArticleController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevLavka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private IBaseRepository<Article> _articleRepository;
        private UserManager<User> _userManager;

        public ArticleController(IBaseRepository<Article> articleRepository, UserManager<User> userManager)
        {
            _articleRepository = articleRepository;
            _userManager = userManager;
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
            if (!await IsAdmin(HttpContext))
                return new TextServerResponse("You must be admin", 500);

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
            if (!await IsAdmin(HttpContext))
                return new TextServerResponse("You must be admin", 500);
            
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
            if (!await IsAdmin(HttpContext))
                return new TextServerResponse("You must be admin", 500);
            
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
        [HttpGet("articles")]
        public async Task<GetArticlesResponse> GetArticles(Specialization? specialization = null, int totalCount = 10, int page = 1)
        {
            User? user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return new GetArticlesResponse(null, 500, "You must be authorized", 0);

            List<Article> EfArticles = _articleRepository.GetAll().ToList();

            if (specialization != null)
            {
                EfArticles = _articleRepository.GetAll().Where(a => a.Specialization == specialization).ToList();
            }

            if (EfArticles.Count == 0)
            {
                return new GetArticlesResponse(new Article[0], 200, null, 0);
            }

            int pagesCount = (int)Math.Ceiling((double)EfArticles.Count / (double)totalCount);

            if (page > pagesCount)
                return new GetArticlesResponse(null, 500, $"Max page is {pagesCount}", 0);

            List<Article> articles = GetArticlesPaginate(totalCount, page, EfArticles);

            return new GetArticlesResponse(articles.ToArray(), 200, null, pagesCount);
        }
        private List<Article> GetArticlesPaginate(int totalCount, int page, List<Article> efArticles)
        {

            int skipCount = totalCount * (page - 1);
            int elementsCount = totalCount;

            int elementsBeforeSkipCount = efArticles.Skip(skipCount).Count();

            if (elementsBeforeSkipCount < totalCount)
            {
                elementsCount = elementsBeforeSkipCount;
            }

            return efArticles.GetRange(skipCount, elementsCount);
        }
        private async Task<bool> IsAdmin(HttpContext context)
        {
            User? user = await _userManager.GetUserAsync(context.User);

            if (user == null || user.Role == RoleType.Client)
                return false;

            else return true;
        }
    }
}