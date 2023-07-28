using ArticlesWebApi.Data;
using ArticlesWebApi.Interfaces;
using ArticlesWebApi.Models;

namespace ArticlesWebApi.Repository
{
    public class ComentsRepository : IComentsRepository
    {
        private readonly DataContext _context;
        public ComentsRepository(DataContext context)
        {
            _context = context;
        }

        public bool ComentExists(int comentId)
        {
            return _context.Coments.Any(a => a.Id == comentId);
        }

        public bool CreateComent(Coments coments)
        {
            _context.Add(coments);
            return Save();
        }

        public bool DeleteComent(Coments coments)
        {
            _context.Remove(coments);
            return Save();
        }

        public Article GetArticalByComent(int comentId)
        {
            var article = _context.Coments.FirstOrDefault(a => a.Id == comentId);
            return article?.Article;
        }

        public Coments GetComent(int comentId)
        {
            return _context.Coments.Where(o => o.Id == comentId).FirstOrDefault();
        }

        public Coments GetComentByArtical(int comentId)
        {
            return _context.Coments.FirstOrDefault(a => a.ArticleId == comentId);
        }

        public ICollection<Coments> GetComments()
        {
            return _context.Coments.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateComent(Coments coments)
        {
            _context.Update(coments);
            return Save();
        }
    }
}
