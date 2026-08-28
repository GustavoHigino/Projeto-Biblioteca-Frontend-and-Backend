using Mapster;
using Microsoft.EntityFrameworkCore;
using projetobiblioteca.Data;
using projetobiblioteca.Data.DTO.Aluno;
using projetobiblioteca.Data.DTO.EmprestimoAluno;
using projetobiblioteca.Data.DTO.Livro;
using projetobiblioteca.Model;
using projetobiblioteca.Pagamentos.Gerador;
using projetobiblioteca.Pagamentos.Model;
using projetobiblioteca.Pagination;
using projetobiblioteca.Repositorios;

namespace projetobiblioteca.Servicos.Implementation
{
    public class EmprestimoAlunoService : IEmprestimoAlunoService
    {
        private readonly IEmprestimoAlunoRepository _repositoryEmprestimoAluno;
        private readonly ILivroService _livroService;
        private readonly IAlunoService _alunoService;
        public EmprestimoAlunoService(IEmprestimoAlunoRepository repositoryEmprestimoAluno
            , ILivroService livroService,
            IAlunoService alunoService) 
        {
            _repositoryEmprestimoAluno = repositoryEmprestimoAluno;
            _livroService= livroService ;
            _alunoService = alunoService ;
        }
        
        public EmprestimoAlunos Returned(long id)
        {
            var emprestimo = _repositoryEmprestimoAluno.ShowById(id);
            if (emprestimo.Devolvido == true)
            {
                return _repositoryEmprestimoAluno.Returned(id);
            }
            var aluno = _alunoService.ShowById(emprestimo.IdAluno);
            aluno.Emprestimos = aluno.Emprestimos - 1;
            _alunoService.Update(aluno.Adapt<AlunoDto>());
            var livro=_livroService.ShowById(emprestimo.IdLivro);
            livro.Emprestados = livro.Emprestados - 1;
            if(DateTime.UtcNow> emprestimo.Fim)
            {
                int diasAtraso = (DateTime.UtcNow.Date -
                    emprestimo.Fim.Date).Days;
                emprestimo.ValorMulta = 50 + (1 * diasAtraso);
                emprestimo.Multado = true;
                _repositoryEmprestimoAluno.Update(emprestimo);

            }

            _livroService.Update(livro.Adapt<LivroDto>());
            return _repositoryEmprestimoAluno.Returned(id);

        }

        public EmprestimoAlunos NotReturned(long id)
        {
            var emprestimo = _repositoryEmprestimoAluno.ShowById(id);
            if(emprestimo.Devolvido == false)
            {
                return _repositoryEmprestimoAluno.NotReturned(id);
            }
            var aluno = _alunoService.ShowById(emprestimo.IdAluno);
            aluno.Emprestimos = aluno.Emprestimos +1;
            _alunoService.Update(aluno.Adapt<AlunoDto>());
            var livro = _livroService.ShowById(emprestimo.IdLivro);
            livro.Emprestados = livro.Emprestados + 1;
            _livroService.Update(livro.Adapt<LivroDto>());
            return _repositoryEmprestimoAluno.NotReturned(id);
        }

        public Task<PaginationClass<EmprestimoAlunos>> PagedList(int ItensPage, long pageCurrently)
        {

            var query = Show().AsQueryable().AsNoTracking()
                .Include(a=>a.Aluno).Include(a=>a.Livro);
            return _repositoryEmprestimoAluno.PagedList(ItensPage, pageCurrently,query);
        }

        public IQueryable<EmprestimoAlunos> Show()
        {
            return _repositoryEmprestimoAluno.Show();
        }

        public EmprestimoAlunos ShowById(long id)
        {
            return _repositoryEmprestimoAluno.ShowById(id);
        }

        public EmprestimoAlunoDto Add(EmprestimoAlunoDto accept)
        {
            var aluno = _alunoService.ShowById(accept.IdAluno);
            if (aluno.Emprestimos > 2)
            {
                return null;
            }
            var livro=_livroService.ShowById(accept.IdLivro);
            if (livro.Disponiveis == 0)
            {
                return null;
            }
            var emprestimoAluno = accept.Adapt<EmprestimoAlunos>();
            aluno.Emprestimos = aluno.Emprestimos + 1;
            _alunoService.Update(aluno.Adapt<AlunoDto>());
            livro.Emprestados = livro.Emprestados+ 1;
            _livroService.Update(livro.Adapt<LivroDto>());
            return _repositoryEmprestimoAluno.Add(emprestimoAluno).Adapt<EmprestimoAlunoDto>();
            
        }

        public EmprestimoAlunoDto Update(EmprestimoAlunoDto accept)
        {
            var emprestimoAluno=accept.Adapt<EmprestimoAlunos>();
            return _repositoryEmprestimoAluno.Update(emprestimoAluno).Adapt<EmprestimoAlunoDto>();
        }

        public EmprestimoAlunos FindByIdQuery(long id)
        {
            return Show().AsNoTracking().AsQueryable()
                .Include(ea => ea.Aluno)
                .Include(ea => ea.Livro)
                .FirstOrDefault(e => e.Id == id);
        }
        public IQueryable<EmprestimoAlunos> FindVencidos(
            )
        {
            return Show().Where(ea => ea.Fim <
            DateTime.UtcNow).AsQueryable().AsNoTracking()
            .Include(a => a.Livro).Include(a => a.Aluno);
        }
        public IQueryable<EmprestimoAlunos> FindMultados(
            )
        {
            return Show().Where(ea => ea.Multado
            ).AsQueryable().AsNoTracking()
            .Include(a => a.Livro).Include(a => a.Aluno);
        }
        public string CriarPix(string valor,long idClient)
        {
            var payload = GeradorPix.MontarPaylodPix
                (new Recebedor
                {
                    ChavePix = Environment.GetEnvironmentVariable("Chave_Pix"),
                    NomeRecebedor = "Narigudo",
                    CidadeRecebedor = "Sao Paulo",
                    ValorPix = valor,
                    TxtId = idClient.ToString()
                });
            return GeradorPix.GerarImagemQRCode(payload);
        }
    }
}
