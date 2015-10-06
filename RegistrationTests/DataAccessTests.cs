using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using RegistrationService.Helpers;
using RegistrationService.Repositories;
using WcfCommon.Domain;

namespace RegistrationTests
{
    [TestClass]
    public class DataAccessTests
    {
        [TestMethod]
        public void CanGenerateSchema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(User).Assembly);

            new SchemaExport(cfg).Execute(false, true, false);
        }

        [TestMethod]
        public void CanAddUser()
        {
            var user = new User
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                CityId = new Guid("A3F997ED-0A93-4A95-B50A-0000025C49F6")
            };
            IUserRepository repository = new UserRepository();
            repository.AddUser(user);

            using (ISession session = NHibernateHelper.OpenSession())
            {
                var fromDb = session.Get<User>(user.Id);
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(user, fromDb);
                Assert.AreEqual(user.FirstName, fromDb.FirstName);
                Assert.AreEqual(user.LastName, fromDb.LastName);
            }
        }

        [TestMethod]
        public void CanGetCities()
        {
            ICityRepository repository = new CityRepository();
            var cities = repository.GetCities(null);
            Assert.IsNotNull(cities);
            Assert.IsTrue(cities.Count > 0);

            cities = repository.GetCities(string.Empty);
            Assert.IsNotNull(cities);
            Assert.IsTrue(cities.Count > 0);

            cities = repository.GetCities("москва");
            Assert.IsNotNull(cities);
            Assert.AreEqual(1, cities.Count);

            cities = repository.GetCities("Москва");
            Assert.IsNotNull(cities);
            Assert.AreEqual(1, cities.Count);

            cities = repository.GetCities("NonexistentCityNamePart");
            Assert.IsNotNull(cities);
            Assert.AreEqual(0, cities.Count);
        }
    }
}
