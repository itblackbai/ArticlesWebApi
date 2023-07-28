using ArticlesWebApi.Models;

namespace ArticlesWebApi.Interfaces
{
    public interface IArticleRepository
    {
        ICollection<Article> GetArticles();

        Article GetArticle(int artId);

        bool ArticleExists(int artId);
        bool CreateArticle(Article article);
        bool UpdateArticle(Article article);

        bool DeleteArticle(Article article);

        bool Save();

        User GetUserByArticle(int articleId);
        Article GetArticleByUser(int articleId);
    }
}
