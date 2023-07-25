namespace ArticlesWebApi.Models
{
    public class ArticalFavorites
    {
        public int UsertId { get; set; }
        public User User { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
