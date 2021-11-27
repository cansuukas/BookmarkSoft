using BookmarkSoft.Core.Map;
using BookmarkSoft.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookmarkSoft.Model.Maps
{
    public class UserMap : CoreMap<User>
    {
     
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.ImageUrl).HasMaxLength(250).IsRequired(false);
            builder.Property(x => x.EmailAddress).HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.Password).HasMaxLength(20).IsRequired(true);
            builder.Property(x => x.LastLogin).IsRequired(false);
            builder.Property(x => x.LastIPAddress).HasMaxLength(15).IsRequired(false);

            base.Configure(builder);
        }
    }
}
