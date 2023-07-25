namespace ArticlesWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }

        public ICollection<Article> Articles { get; set;}
       
        public ICollection<ArticalFavorites> ArticalFavorites { get; set; }
    }
}
