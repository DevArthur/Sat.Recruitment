using Sat.Recruitment.Persistence.Models;
using System.Collections.Generic;

namespace Sat.Recruitment.Service.Service
{
    public interface IUserService
    {
        /// <summary>
        /// It applies a percentage based on user type.
        /// </summary>
        /// <param name="user">User that is trying to insert.</param>
        /// <returns></returns>
        User ApplyPercentageByUserType(User user);

        /// <summary>
        /// It checks if is the user trying to insert already exists.
        /// </summary>
        /// <param name="user">User that is trying to insert.</param>
        /// <param name="users">List of existing users.</param>
        /// <returns>bool</returns>
        bool IsDuplicatedUser(User user, IEnumerable<User> users);
    }
}
