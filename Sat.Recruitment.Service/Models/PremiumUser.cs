using Sat.Recruitment.Persistence.Models;

namespace Sat.Recruitment.Service.Models
{
    /// <summary>
    /// Premium user class.
    /// </summary>
    public class PremiumUser : Users
    {
        private readonly User _user;

        public PremiumUser(User user)
        {
            _user = user;
        }

        /// <summary>
        /// It applies a percentage for a premium type of user.
        /// </summary>
        /// <returns>User</returns>
        public override User ApplyPercentage()
        {
            _user.Money *= MoneyPercentages.Premium2;
            return _user;
        }
    }
}
