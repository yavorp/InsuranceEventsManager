using InsuranceManager.DataAccess;
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
    }
}
