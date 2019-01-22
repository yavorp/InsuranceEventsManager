using InsuranceManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext() : base("AccessDbContext")
        {
            Database.SetInitializer<DataContext>(new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>());
        }
       
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public void WithDataContext(Action<DataContext> action)
        {
            using (var context = new DataContext())
            {
                action(context);
            }
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }


        public void Test()
        {
            WithDataContext(context =>
            {
                context.Accidents.Where(a => a.Id == 0).ToList();
                //SELECT * FROM Accidents INNER JOIN Object ON Id = Object.Id WHERE Id = 0
            });
        }

    }
}
