using System.Collections.Generic;
using RegistrationService.Repositories;
using WcfCommon.Domain;

namespace RegistrationService
{
    public class RegistrationService : IRegistrationService
    {
        private IUserRepository _userRepository;
        private IUserRepository UserRepository {
            get { return _userRepository ?? (_userRepository = new UserRepository()); }
        }

        private ICityRepository _cityRepository;
        private ICityRepository CityRepository
        {
            get { return _cityRepository ?? (_cityRepository = new CityRepository()); }
        }

        public IList<City> GetCities(string term)
        {
            return CityRepository.GetCities(term);
        }

        public void AddUser(User user)
        {
            UserRepository.AddUser(user);
        }
    }
}
