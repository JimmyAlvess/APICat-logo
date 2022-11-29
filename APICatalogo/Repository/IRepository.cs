<<<<<<< HEAD
﻿using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        T GetById(Expression<Func<T,bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
=======
﻿namespace APICatalogo.Repository
{
    public interface IRepository
    {
>>>>>>> efcac92997fb0368873cebe92508688ccd31aaf2
    }
}
