using Sat.Recruitment.Persistence.Models;

namespace Sat.Recruitment.Service.Models
{
    /// <summary>
    /// Super user class.
    /// </summary>
    public class SuperUser : Users
    {
        private readonly User _user;

        public SuperUser(User user)
        {
            _user = user;
        }

        /// <summary>
        /// It applies a percentage for a super type of user.
        /// </summary>
        /// <returns>User</returns>
        public override User ApplyPercentage()
        {
            _user.Money *= MoneyPercentages.SuperUser20;
            return _user;
        }
    }
}
