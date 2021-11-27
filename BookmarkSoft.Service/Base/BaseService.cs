using BookmarkSoft.Core.Entity;
using BookmarkSoft.Core.Entity.Enums;
using BookmarkSoft.Core.Service;
using BookmarkSoft.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;

namespace BookmarkSoft.Service.Base
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        private readonly BookmarkSoftContext _context;

        public BaseService(BookmarkSoftContext context)
        {
            _context = context;
        }

        public bool Activate(Guid id)
        {
            T active = GetById(id);
            active.Status = Status.Active;
            return Update(active);
        }

        public bool Add(T item)
        {
            try
            {
                _context.Set<T>().Add(item);
                return Save() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Add(List<T> items)
        {
            try
            {
              
                using (TransactionScope ts = new TransactionScope())
                {
                    _context.Set<T>().AddRange(items);
                    ts.Complete();
                    return Save() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Any(Expression<Func<T, bool>> expression) => _context.Set<T>().Any(expression); 
      

        public List<T> GetActive() => _context.Set<T>().Where(x => x.Status != Status.Deleted || x.Status != Status.None).ToList();
      

        public List<T> GetAll() => _context.Set<T>().ToList();
     

        public T GetByDefault(Expression<Func<T, bool>> expression) => _context.Set<T>().FirstOrDefault(expression);
      

        public T GetById(Guid id) => _context.Set<T>().Find(id);
    

        public List<T> GetDefault(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression).ToList();
     

        public bool Remove(T item)
        {
            try
            {
                _context.Set<T>().Remove(item);
                return Save() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(Guid id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    T item = GetById(id);
                    _context.Set<T>().Remove(item);
                    ts.Complete();
                    return Save() > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetDefault(expression);
                    int count = 0;
                    foreach (var item in collection)
                    {
                        bool result = Remove(item);
                        if (result)
                        {
                            count++;
                        }
                    }

                    if (collection.Count == count)
                    {
                        ts.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T item)
        {
            try
            {
                _context.Set<T>().Update(item);
                return Save() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}