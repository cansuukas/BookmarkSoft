using BookmarkSoft.Core.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookmarkSoft.Core.Entity
{
   public class CoreEntity : IEntity<Guid>
    {
        public Guid ID { get ; set ; } 
        public Status Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIP { get; set; }


    }
} 
