﻿
using BookmarkSoft.Core.Entity;
using BookmarkSoft.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookmarkSoft.Model.Entities
{
    public class Post : CoreEntity
    {
        public string Title { get; set; }
        public string PostDetail { get; set; }
        public string Tags { get; set; }
        public string ImagePath { get; set; }
        public int ViewCount { get; set; }

        public Guid CategoryID { get; set; }
        public virtual Category Category { get; set; }  

        public Guid UserID { get; set; } 
        public virtual User User { get; set; }
    }
}