namespace ArticlesWebApi.Models
{
    public class Coments
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
