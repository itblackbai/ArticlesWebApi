namespace ArticlesWebApi.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }



        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Coments> Coments { get; set; }

        public ICollection<ArticleTags> ArticleTags { get; set; }

        public ICollection<ArticalFavorites> ArticalFavorites { get; set;}
    }
}
