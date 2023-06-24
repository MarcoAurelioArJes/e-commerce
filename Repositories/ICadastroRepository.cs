using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public interface ICadastroRepository
    {
        Cadastro Atualiza(int idCadastro, Cadastro novoCadastro);
    }
}