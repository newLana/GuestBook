using System.Data.Entity;

namespace GuestBook.Models.DAL.EF
{
    public class GuestBookContext:DbContext
    {
        public GuestBookContext():base("Default"){}

        public DbSet<Record> Records { get; set; }
    }
}