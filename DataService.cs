using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext _contexto;
        private readonly IProdutoRepository _produtoRepository;

        public DataService(ApplicationContext contexto, 
                           IProdutoRepository produtoRepository)
        {
            _contexto = contexto;
            _produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            _contexto.Database.Migrate();

            List<Livro> livros = ObterLivros();

            _produtoRepository.SalvarProdutos(livros);
        }

        private static List<Livro> ObterLivros()
        {
            var jsonDeLivros = File.ReadAllText("livros.json");
            var livros = JsonConvert.DeserializeObject<List<Livro>>(jsonDeLivros);
            return livros;
        }
    }
}
