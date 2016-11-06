using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EntityFrameworkDbContext : DbContext
    {
        public virtual DbSet<Persons> Persons { get; set; }

        public EntityFrameworkDbContext(): base("name=DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //禁用一对多级联删除，如果存在联级删除，请通过程序实现
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //禁用多对多级联删除，如果存在联级删除，请通过程序实现
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
