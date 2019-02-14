using InsuranceManager.DataAccess;
using System.Data.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagerUnitTests
{
    public class DataContextTests
    {
        [Test]
        public void ShouldConnectToDatabase()
        {
            Assert.DoesNotThrow(() => {
                using (var context = new DataContext())
                {

                }
            });
        }

        [Test]
        public void ShouldReturnNotEmpty()
        {
            using (var context = new DataContext())
            {
                var accidents = context.Accidents.ToList();
                var mechanics = context.Mechanics.ToHashSet();
                Assert.IsTrue(accidents.Count!=0);
            }
           
        }

        [Test]
        public void ShouldReturnAllMechanics()
        {
            using (var context = new DataContext())
            {
                var workshops = context.Workshops.Include(t=>t.Mechanics.Select(m=>m.Tasks)).ToList();

                Assert.IsTrue(workshops[0].Mechanics.Count>0);
            }
        }

        [Test]
        public void ShouldReturnTasks()
        {
            using (var context = new DataContext())
            {
                var tasks = context.Tasks.ToList();

                Assert.IsTrue(tasks.Count>0);
            }
        }
    }
}
