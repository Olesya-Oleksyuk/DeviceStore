using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
  {

    // We have the access to the current <T> type - which is gonna be replaced with the given Entity. 

    private readonly StoreContext _context;
    public GenericRepository(StoreContext context)
    {
      _context = context;
    }

    public async Task<T> GetByIdAsync(int id)
    {
      return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
      return await _context.Set<T>().ToListAsync();
    }




    public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
    {
      return await ApplySpecification(spec).FirstOrDefaultAsync();
    }


    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
      return await ApplySpecification(spec).ToListAsync();
    }



    /// <summary>
    /// Allows to apply the Specification to the given Entity using SpecificationEvaluator
    /// </summary>
    /// <param name="spec"></param>
    /// <returns>IQueryable object that is used by async method to execute a query on the DB</returns>
    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
      // return the IQueryable<T> by calling the static method  
      /* 
      Заметка:
        Так как статический метод GetQuery() требует 1ым параметром - IQueryable<TEntity> inputQuery
        (т.е. объект типа IQueryable замкнутый на текущую сущность экземпляра репозитория),то мы можем
        достучаться до TEntity текущего экземпляра репозитория через _context.Set<T> и конвертировать этот 
        Set<T> в IQueryable с помощью AsQueryable().
      */
      return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }
  }
}