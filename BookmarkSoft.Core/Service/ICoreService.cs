using BookmarkSoft.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace BookmarkSoft.Core.Service
{
  public  interface ICoreService<T> where T: CoreEntity
    {
        bool Add(T item);
        bool Add(List<T> items);
        bool Update(T item);
        bool Remove(T item);
        bool Remove(Guid id);
        bool RemoveAll(Expression<Func<T, bool>> expression); //toplu silme işlemi
        T GetById(Guid id); //id'ye göre getir
        T GetByDefault(Expression<Func<T, bool>> expression);
        List<T> GetActive(); //kullanıcının aktif olup olmaması
        List<T> GetDefault(Expression<Func<T, bool>> expression);
        List<T> GetAll();
        bool Activate(Guid id);
        bool Any(Expression<Func<T, bool>> expression);
        int Save();

    }
}
