using Sat.Recruitment.Persistence.Models;

namespace Sat.Recruitment.Service.Models
{
    /// <summary>
    /// Normal user class.
    /// </summary>
    public class NormalUser : Users
    {
        private readonly User _user;

        public NormalUser(User user)
        {
            _user = user;
        }

        /// <summary>
        /// It applies a percentage for a normal type of user.
        /// </summary>
        /// <returns>User</returns>
        public override User ApplyPercentage()
        {
            //If new user is normal and has more than USD100
            if (_user.Money > 100)
            {
                _user.Money *= MoneyPercentages.Normal12;
            }
            else if (_user.Money > 10)
            {
                _user.Money *= MoneyPercentages.Normal8;
            }
            return _user;
        }
    }
}
