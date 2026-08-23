using projetobiblioteca.Context;
using projetobiblioteca.Model;

namespace projetobiblioteca.Repositorios.Implementations
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        public UserRepository(MSSQL context) : 
            base(context)
        {
        }

  
        

        public Users FindByKey(string key)
        {
            var user =_context.Users.FirstOrDefault(u => u.Key == key);
            return user;
        }

        public Users FindByUserName(string username)
        {
            var user=_context.Users.FirstOrDefault
                (p => p.Username == username);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public bool RevokeToken(string username)
        {
            var user = FindByUserName(username);
            if (user == null)
            {
                return false;
            }
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }
    }
}
