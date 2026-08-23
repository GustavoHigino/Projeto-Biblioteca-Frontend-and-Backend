using projetobiblioteca.Model;

namespace projetobiblioteca.Repositorios
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        public Users FindByUserName(string username);
        public bool RevokeToken(string username);
        public Users FindByKey(string key);
    }
}
