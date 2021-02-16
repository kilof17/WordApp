namespace WordApp.Models
{
    using System.Data.Entity;

    public class WordAppEntities : DbContext
    {
        public WordAppEntities()
            : base("name=WordAppEntities")
        {
        }
           
            public virtual DbSet<User> Users { get; set; }
            public virtual DbSet<DBcontrol> DBcontrols { get; set; }
            public virtual DbSet<Format> Formats { get; set; }
        }
    }


  