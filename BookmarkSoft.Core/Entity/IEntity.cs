using System;
using System.Collections.Generic;
using System.Text;

namespace BookmarkSoft.Core.Entity
{
   
    public interface IEntity<T>
    {
        T ID { get; set; }
    }
}