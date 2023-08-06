using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public interface ICommonRepository<T>
{
    //t- list of employee, event, role, user
    List<T> GetAll();
    // t - single object (prim key) (employee id, event id, role id, user id)
    T GetDetails(int id);
    void Insert(T item);
    void Update(T item);
    void Delete(T item);
    int SaveChanges();
}
