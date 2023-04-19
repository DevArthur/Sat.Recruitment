using Sat.Recruitment.Persistence.Models;
using Sat.Recruitment.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Service.Service
{
    public class UserService : IUserService
    {
        /// <summary>
        /// It applies a percentage based on user type.
        /// </summary>
        /// <param name="user">User that is trying to insert.</param>
        /// <returns></returns>
        public User ApplyPercentageByUserType(User user)
        {
            if (user.UserType == null)
            {
                // TODO: Missing logic for this scenario: user.UserType == null
                return user;
            }

            switch (user.UserType)
            {
                case UserTypes.Normal:

                    Users normalUser = new NormalUser(user);
                    user = normalUser.ApplyPercentage();
                    break;

                case UserTypes.SuperUser:

                    Users superUser = new SuperUser(user);
                    user = superUser.ApplyPercentage();
                    break;

                case UserTypes.Premium:

                    Users premiumUser = new PremiumUser(user);
                    user = premiumUser.ApplyPercentage();
                    break;

                default:
                    break;
            }

            return user;
        }

        /// <summary>
        /// It checks if is the user trying to insert already exists.
        /// </summary>
        /// <param name="user">User that is trying to insert.</param>
        /// <param name="users">List of existing users.</param>
        /// <returns>bool</returns>
        public bool IsDuplicatedUser(User user, IEnumerable<User> users)
        {
            return users.Any(x => x.Name.Equals(user.Name) &&
                x.Email.Equals(user.Email) &&
                x.Phone.Equals(user.Phone) &&
                x.Address.Equals(user.Address));
        }
    }
}
