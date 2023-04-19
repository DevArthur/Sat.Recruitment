using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Persistence.Models;
using Sat.Recruitment.Persistence.Repository;
using Sat.Recruitment.Service.Models;
using Sat.Recruitment.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserUnitTest
    {
        private readonly Mock<IRepository<User>> _repository;
        private readonly Mock<IUserService> _userService;
        private readonly UsersController _usersController;
        private readonly List<User> userList;
        private readonly User user;

        public UserUnitTest()
        {
            _repository = new Mock<IRepository<User>>();
            _userService = new Mock<IUserService>();
            _usersController = new UsersController(_userService.Object, _repository.Object);

            userList = new List<User>()
            {
                new User() { Id = 1, Name = "Juan", Email="Juan@marmol.com",
                Address="Peru 2464", Phone="+5491154762312", UserType="Normal", Money=1234.00M },

                new User() { Id = 2, Name = "Franco", Email="Franco.Perez@gmail.com",
                Address="Alvear y Colombres", Phone="+534645213542", UserType="Premium", Money=987987.00M },

                new User() { Id = 2, Name = "Agustina", Email="Agustina@gmail.com",
                Address="Garay y Otra Calle", Phone="+534645213542", UserType="SuperUser", Money=112234.00M }
            };

            user = new User()
            {
                Id = 1,
                Name = "Juan",
                Email = "Juan@marmol.com",
                Address = "Peru 2464",
                Phone = "+5491154762312",
                UserType = "Normal",
                Money = 1234.00M
            };
        }

        [Fact]
        public void Get_AllUsers_Result_Type()
        {
            // This is the result expected for mock
            var task = Task.FromResult<IEnumerable<User>>(userList);
            _repository.Setup(x => x.GetAll()).Returns(task);

            // Execution
            var users = _usersController.GetUsers();

            // Assertions
            Assert.IsType<List<User>>(users.Result);
        }

        [Fact]
        public void Get_AllUsers_Result_Quantity()
        {
            // This is the result expected for mock
            var task = Task.FromResult<IEnumerable<User>>(userList);
            _repository.Setup(x => x.GetAll()).Returns(task);

            // Execution
            var users = _usersController.GetUsers();

            // Assertions
            Assert.Equal(userList.Count, users.Result.Count());
        }

        [Fact]
        public void Get_AllUsers_ResultId_Expected()
        {
            // This is the result expected for mock
            var task = Task.FromResult<IEnumerable<User>>(userList);
            _repository.Setup(x => x.GetAll()).Returns(task);

            // Execution
            var response = _usersController.GetUsers();

            // Assertions
            Assert.Equal(userList.First().Id, response.Result.First().Id);
        }

        [Fact]
        public void Get_UserById()
        {
            // This is the result expected for mock
            int id = 1;
            var task = Task.FromResult(user);
            _repository.Setup(x => x.Get(id)).Returns(task);

            // Execution
            var response = _usersController.GetUser(id);

            // Assertions            
            Assert.IsType<OkObjectResult>(response.Result.Result);
        }

        [Fact]
        public void Get_UserById_No_Existis()
        {
            int id = 6;
            // This is the result expected for mock
            var task = Task.FromResult((User)null);
            _repository.Setup(x => x.Get(id)).Returns(task);

            // Execution
            var expectedUser = _usersController.GetUser(id);

            // Assertions            
            Assert.IsType<NotFoundResult>(expectedUser.Result.Result);
        }

        [Fact]
        public void Update_User()
        {
            // This is the result expected for mock            
            _repository.Setup(x => x.Update(user));
            _repository.Setup(x => x.Save());

            var response = _usersController.UpdateUsers(user);

            // Assertions
            Assert.IsType<OkResult>(response.Result);
        }

        [Fact]
        public void Update_User_No_Exists()
        {
            // This is the result expected for mock            
            _repository.Setup(x => x.Update(user)).Throws(new Exception("Issue trying to update the user"));

            // Execution
            var expectedUser = _usersController.UpdateUsers(user);

            // Assertions            
            Assert.Contains("Issue trying to update the user", expectedUser.Exception.InnerException.Message);
        }

        [Fact]
        public void Create_User()
        {
            // This is the result expected for mock                        
            _userService.Setup(x => x.ApplyPercentageByUserType(user)).Returns(user);
            var task = Task.FromResult<IEnumerable<User>>(userList);
            _repository.Setup(x => x.GetAll()).Returns(task);
            _userService.Setup(x => x.IsDuplicatedUser(user, userList)).Returns(false);
            _repository.Setup(x => x.Add(user));
            _repository.Setup(x => x.Save());

            var response = _usersController.CreateUsers(user);

            // Assertions
            Assert.NotNull(response);
            Assert.IsType<OkResult>(response.Result);
        }

        [Fact]
        public void Create_Duplicated_User()
        {
            // This is the result expected for mock                        
            _userService.Setup(x => x.ApplyPercentageByUserType(user)).Returns(user);
            var task = Task.FromResult<IEnumerable<User>>(userList);
            _repository.Setup(x => x.GetAll()).Returns(task);
            _userService.Setup(x => x.IsDuplicatedUser(user, userList)).Returns(true);

            var response = _usersController.CreateUsers(user);

            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public void Create_NormalUser_Validate_Amout_Calculated()
        {
            Users normalUser = new NormalUser(user);
            var expectedUser = normalUser.ApplyPercentage();
            // This is the result expected for mock                        
            _userService.Setup(x => x.ApplyPercentageByUserType(user)).Returns(expectedUser);
            var task = Task.FromResult<IEnumerable<User>>(userList);
            _repository.Setup(x => x.GetAll()).Returns(task);
            _userService.Setup(x => x.IsDuplicatedUser(user, userList)).Returns(false);
            _repository.Setup(x => x.Add(expectedUser));
            _repository.Setup(x => x.Save());

            var response = _usersController.CreateUsers(user);

            // Assertions
            Assert.Equal(148.0800M, user.Money);
            Assert.IsType<OkResult>(response.Result);
        }

        [Fact]
        public void Create_SuperUser_Validate_Amout_Calculated()
        {
            Users superUser = new SuperUser(user);
            var expectedUser = superUser.ApplyPercentage();
            // This is the result expected for mock                        
            _userService.Setup(x => x.ApplyPercentageByUserType(user)).Returns(expectedUser);
            var task = Task.FromResult<IEnumerable<User>>(userList);
            _repository.Setup(x => x.GetAll()).Returns(task);
            _userService.Setup(x => x.IsDuplicatedUser(user, userList)).Returns(false);
            _repository.Setup(x => x.Add(expectedUser));
            _repository.Setup(x => x.Save());

            var response = _usersController.CreateUsers(user);

            // Assertions
            Assert.Equal(246.8000M, user.Money);
            Assert.IsType<OkResult>(response.Result);
        }

        [Fact]
        public void Create_PremiumUser_Validate_Amout_Calculated()
        {
            Users premiumUser = new PremiumUser(user);
            var expectedUser = premiumUser.ApplyPercentage();
            // This is the result expected for mock                        
            _userService.Setup(x => x.ApplyPercentageByUserType(user)).Returns(expectedUser);
            var task = Task.FromResult<IEnumerable<User>>(userList);
            _repository.Setup(x => x.GetAll()).Returns(task);
            _userService.Setup(x => x.IsDuplicatedUser(user, userList)).Returns(false);
            _repository.Setup(x => x.Add(expectedUser));
            _repository.Setup(x => x.Save());

            var response = _usersController.CreateUsers(user);

            // Assertions
            Assert.Equal(2468.00M, user.Money);
            Assert.IsType<OkResult>(response.Result);
        }

        [Fact]
        public void Delete_User()
        {
            var id = 1;
            // This is the result expected for mock
            var task = Task.FromResult(user);
            _repository.Setup(x => x.Get(id)).Returns(task);
            _repository.Setup(x => x.Delete(id));
            _repository.Setup(x => x.Save());

            var response = _usersController.DeleteUsers(id);

            // Assertions           
            Assert.IsType<OkResult>(response.Result);
        }

        [Fact]
        public void Delete_User_No_Exists()
        {
            var id = 1;
            // This is the result expected for mock
            var task = Task.FromResult((User)null);
            _repository.Setup(x => x.Get(id)).Returns(task);

            var response = _usersController.DeleteUsers(id);

            // Assertions           
            Assert.IsType<NotFoundResult>(response.Result);
        }
    }
}
