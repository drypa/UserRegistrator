using NHibernate;
using RegistrationService.Helpers;
using WcfCommon.Domain;

namespace RegistrationService.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(user);
                transaction.Commit();
            }
        }
    }
}