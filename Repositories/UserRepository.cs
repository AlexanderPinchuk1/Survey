using System.Collections.Generic;
using iTechArt.SurveyCreator.Models;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace iTechArt.SurveyCreator.Repositories
{
    public class UserRepository: IRepository<User>
    {
        private readonly ApplicationContext _db;
        

        public UserRepository(ApplicationContext context)
        {
            _db = context;
        }


        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        public void Create(User user)
        {
            _db.Users.Add(user);
        }

        public void Update(User user)
        {
            _db.Entry(user).State = (EntityState) System.Data.Entity.EntityState.Modified;
        }

        public void Delete(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                _db.Users.Remove(user);
            }
        }
    }
}
