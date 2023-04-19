using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Persistence.Models;

namespace Sat.Recruitment.Persistence.DataSeeding
{
    /// <summary>
    /// This class is for seeding data into the database
    /// </summary>
    public static class DatabaseData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var juan = new User
            {
                Id = 1,
                Name = "Juan",
                Email = "Juan@marmol.com",
                Address = "Peru 2464",
                Phone = "+5491154762312",
                UserType = "Normal",
                Money = 1234
            };

            var franco = new User
            {
                Id = 2,
                Name = "Franco",
                Email = "Franco.Perez@gmail.com",
                Address = "Alvear y Colombres",
                Phone = "+534645213542",
                UserType = "Premium",
                Money = 112234
            };

            var agustina = new User
            {
                Id = 3,
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Garay y Otra Calle",
                Phone = "+534645213542",
                UserType = "SuperUser",
                Money = 112234
            };

            modelBuilder.Entity<User>().HasData(juan, franco, agustina);
        }
    }
}
