using CasaDoCodigo.Models;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class CadastroRepository : BaseRepository<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public Cadastro Atualiza(int idCadastro, Cadastro novoCadastro)
        {
            var cadastroDB = _dbSet
                                .Where(c => c.Id == idCadastro)
                                .SingleOrDefault();

            cadastroDB.Atualiza(novoCadastro);
            _contexto.SaveChanges();
            return cadastroDB;
        }
    }
}
