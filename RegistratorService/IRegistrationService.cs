using System.Collections.Generic;
using System.ServiceModel;
using WcfCommon.Domain;

namespace RegistrationService
{
    [ServiceContract]
    public interface IRegistrationService
    {
        [OperationContract]
        IList<City> GetCities(string term);

        [OperationContract]
        void AddUser(User composite);
    }

}
