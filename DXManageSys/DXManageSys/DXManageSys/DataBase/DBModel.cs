using DataBase.Modules;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataBase
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<user> user { get; set; }

        public virtual DbSet<mail> mail { get; set; }

        public virtual DbSet<pl_head> pl_head { get; set; }

        public virtual DbSet<pl_detail> pl_detail { get; set; }

        public virtual DbSet<pl_cd> pl_cd { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.userId)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.userPassword)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.auth)
                .IsUnicode(false);
        }
    }
}
