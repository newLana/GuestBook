using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuestBook.Models.DAL.EF
{
    public class GuestBookContext:DbContext
    {
        public GuestBookContext():base("Default"){}

        public DbSet<Record> Records { get; set; }
    }
}