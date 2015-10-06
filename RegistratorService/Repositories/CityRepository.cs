using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using RegistrationService.Helpers;
using WcfCommon.Domain;

namespace RegistrationService.Repositories
{
    public class CityRepository : ICityRepository
    {
        public IList<City> GetCities(string term)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var criteria = session.Query<City>();
                if (!string.IsNullOrEmpty(term))
                {
                    return criteria.Where(c => c.Name.Contains(term)).ToList();
                }
                return criteria.ToList();
            }
        }
    }
}