using ArticlesWebApi.Models;

namespace ArticlesWebApi.Interfaces
{
    public interface IComentsRepository
    {
        ICollection<Coments> GetComments();

        Coments GetComent(int comentId);

        bool ComentExists(int comentId);
        bool CreateComent(Coments coments);
        bool UpdateComent(Coments coments);

        bool DeleteComent(Coments coments);

        bool Save();

        Article GetArticalByComent(int comentId);
        Coments GetComentByArtical(int comentId);
    }
}
