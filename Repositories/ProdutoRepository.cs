using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext contexto) 
            : base(contexto)
        {
        }

        public List<Produto> ObterProdutos()
        {
            return _dbSet.ToList();
        }

        public void SalvarProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
                if (!_dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                    _dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));

            _contexto.SaveChanges();
        }
    }


    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
