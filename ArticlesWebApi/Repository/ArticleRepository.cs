using ArticlesWebApi.Data;
using ArticlesWebApi.Interfaces;
using ArticlesWebApi.Models;

namespace ArticlesWebApi.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _context;
        public ArticleRepository(DataContext context)
        {
            _context = context;
        }

        public bool ArticleExists(int artId)
        {
            return _context.Articles.Any(a => a.Id == artId);
        }

        public bool CreateArticle(Article article) 
        {
        
            _context.Add(article);
            return Save();
        }

        public bool DeleteArticle(Article article)
        {
            _context.Remove(article);
            return Save();
        }

       

        public ICollection<Article> GetArticles()
        {
            return _context.Articles.ToList();
        }

        public Article GetArticle(int artId)
        {
            return _context.Articles.Where(o => o.Id == artId).FirstOrDefault();
        }

       
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateArticle(Article article)
        {
            _context.Update(article);
            return Save();
        }

        public User GetUserByArticle(int articleId)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == articleId);
            return article?.User;
        }

        public Article GetArticleByUser(int articleId)
        {
            return _context.Articles.FirstOrDefault(a => a.UserId == articleId);
        }
    }
}
