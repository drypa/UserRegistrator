using WcfCommon.Domain;

namespace RegistrationService.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
    }
}
