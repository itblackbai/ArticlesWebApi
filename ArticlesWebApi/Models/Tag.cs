namespace ArticlesWebApi.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ArticleTags> ArticleTags { get; set; }
    }
}
