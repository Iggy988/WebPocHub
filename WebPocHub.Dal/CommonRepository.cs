using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPocHub.Dal;

namespace Dal
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly WebPocHubDbContext _dbContext;
        private DbSet<T> _table;


        public CommonRepository(WebPocHubDbContext context)
        {
            _dbContext = context;
            _table = _dbContext.Set<T>();
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetDetails(int id)
        {
            return _table.Find(id);
        }

        public void Insert(T item)
        {
            _table.Add(item);
        }

        public void Update(T item)
        {
            _table.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(T item)
        {
            _table.Remove(item);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        
    }
}
