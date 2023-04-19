using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Validations;
using Sat.Recruitment.Persistence.Models;
using Sat.Recruitment.Persistence.Repository;
using Sat.Recruitment.Service.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRepository<User> _userRepository;

        public UsersController(IUserService userService = null,
            IRepository<User> userRepository = null)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("/users")]
        public async Task<IEnumerable<User>> GetUsers() =>
            await _userRepository.GetAll();

        [HttpGet]
        [Route("/users/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        [Route("/users")]
        [ServiceFilter(typeof(UserValidator))]
        public async Task<IActionResult> UpdateUsers(User user)
        {
            try
            {
                _userRepository.Update(user);
                await _userRepository.Save();
            }
            catch (Exception)
            {
                throw new Exception($"Issue trying to update the user");
            }
            return Ok();
        }

        [HttpPost]
        [Route("/users")]
        [ServiceFilter(typeof(UserValidator))]
        public async Task<IActionResult> CreateUsers([FromBody] User user)
        {
            user = _userService.ApplyPercentageByUserType(user);
            var users = await _userRepository.GetAll();

            var isDuplicatedUser = _userService.IsDuplicatedUser(user, users);
            if (!isDuplicatedUser)
            {
                await _userRepository.Add(user);
                await _userRepository.Save();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("/users")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var userExists = await _userRepository.Get(id);
            if (userExists == null)
            {
                return NotFound();
            }
            await _userRepository.Delete(id);
            await _userRepository.Save();
            return Ok();
        }
    }
}
