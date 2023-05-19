using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Repositories
{
    public class BaseRepository<T> where T : BaseModel
    {
        protected readonly ApplicationContext _contexto;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationContext contexto)
        {
            _contexto = contexto;
            _dbSet = _contexto.Set<T>();
        }
    }
}
