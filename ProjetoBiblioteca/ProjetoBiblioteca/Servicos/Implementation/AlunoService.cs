using projetobiblioteca.Data;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class AlunoService : IAlunoService

    {
        private readonly IAlunoRepository _alunoRepository;
        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public IQueryable<Fundionario> Show()
        {
            return _alunoRepository.Show();
        }

        public Fundionario ShowById(long id)
        {
            return _alunoRepository.ShowById(id);
        }

        public Fundionario Add(Fundionario entity)
        {
            return _alunoRepository.Add(entity);
        }

        public Fundionario Update(Fundionario entity)
        {
            return _alunoRepository.Update(entity);
        }

        public bool EnableOrDisable(bool enableOrDisable)
        {
            throw new NotImplementedException();
        }
    }
}
