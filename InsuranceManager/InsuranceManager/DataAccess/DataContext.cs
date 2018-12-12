using InsuranceManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext() : base() { }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public void WithDataContext(Action<DataContext> action)
        {
            using (var context = new DataContext())
            {
                action(context);
            }
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
