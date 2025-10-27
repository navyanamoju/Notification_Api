using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationsApi.Models;
using System.Text.Json;

namespace NotificationsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
      
        private readonly string _filePath = "users.json";
        private object message;

        private List<User> ReadUsers()
        {
            List<User> users = new();
            if (System.IO.File.Exists(_filePath))
            {
                var json = System.IO.File.ReadAllText(_filePath);
                users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
                return users;

            }
            else
            {
                return new List<User>();
            }

        }


        private void SaveUsers(List<User> users)
        {
            var newJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(_filePath, newJson);


        }

        //[HttpPost]
        //public async Task<string> CreateUser(User user)
        //{
        //    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        //    _appDbContext.Users.Add(user);
        //    await _appDbContext.SaveChangesAsync();
        //    return "Success";
        //}

        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> users = ReadUsers();
            return Ok(users);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    List<User> users = ReadUsers();
        //    var user = users.FirstOrDefault(x => x.Id == id);
        //    return Ok(user);
        //}

        [HttpPost]
        [Route("addJson")]
        public IActionResult AddUser(User user)
        {

            List<User> users = ReadUsers();
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            users.Add(user);
            SaveUsers(users);
            return Ok(new { message=" Registered Successfully " });

        }


        //[HttpPut("{id}")]
        //public IActionResult updateUser(int id, User updatedUser)
        //{

        //    List<User> existUsers = ReadUsers();
        //    var user = existUsers.FirstOrDefault(x => x.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound(new { message = "User not found" });
        //    }
        //    user.Age = updatedUser.Age;
        //    user.Name = updatedUser.Name;
        //    user.Password = BCrypt.Net.BCrypt.HashPassword(updatedUser.Password);
        //    user.Company = updatedUser.Company;
        //    SaveUsers(existUsers);
        //    return Ok(new { message = "Success" });
        //}

        [HttpPost("login")]
        public IActionResult loginUser(string username, string password)
        {
            List<User> users = ReadUsers();
            var user = users.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return NotFound(new { message = "user not found" });
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isValid)
            {
                return Unauthorized("Incorrect password");

            }
            return Ok(new { message = "User LoggedIn Successfully" });

        }
    }
}
