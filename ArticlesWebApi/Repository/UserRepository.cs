using ArticlesWebApi.Data;
using ArticlesWebApi.Interfaces;
using ArticlesWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticlesWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

       
    }
}
