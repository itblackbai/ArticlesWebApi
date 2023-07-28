using ArticlesWebApi.Data;
using ArticlesWebApi.Interfaces;
using ArticlesWebApi.Models;

namespace ArticlesWebApi.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;

        public TagRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public bool CreateTag(Tag tag)
        {
            _context.Add(tag);
            return Save();
        }

        public bool DeleteTag(Tag tag)
        {
            _context.Remove(tag);
            return Save();
        }

        public Tag GetTag(int tagId)
        {
            return _context.Tags.Where(t => t.Id == tagId).FirstOrDefault();
        }

        public ICollection<Tag> GetTags()
        {
            return _context.Tags.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TagExists(int tagId)
        {
            return _context.Tags.Any(u => u.Id == tagId);
        }

        public bool UpdateTag(Tag tag)
        {
            _context.Update(tag);
            return Save();
        }


    }
}
