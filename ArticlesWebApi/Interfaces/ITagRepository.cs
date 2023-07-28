using ArticlesWebApi.Models;

namespace ArticlesWebApi.Interfaces
{
    public interface ITagRepository
    {
        ICollection<Tag> GetTags();

        Tag GetTag(int tagId);
        bool TagExists(int tagId);

        bool CreateTag(Tag tag);

        bool UpdateTag(Tag tag);

        bool DeleteTag(Tag tag);

        bool Save();
    }
}
