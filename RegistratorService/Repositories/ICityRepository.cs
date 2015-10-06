using System.Collections.Generic;
using WcfCommon.Domain;

namespace RegistrationService.Repositories
{
    public interface ICityRepository
    {
        IList<City> GetCities(string term);
    }
}
