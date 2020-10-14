using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SampleRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> userList = new List<User>() {
            new User(new Guid("0ec5e125-5eee-4102-b48d-011a600fd74a"), "Norris", "Chuck", new DateTime(1940, 3, 10)){ Username = "Braddock", Descr = "The real superhero" },
            new User(new Guid("8f8cfba1-b30e-4289-a7ca-3aa122adb30a"), "Wayne", "Bruce", new DateTime(1939, 3, 30)){ Username = "Batman", Descr = "Just another geek" },
            new User(new Guid("3fd45a08-c46e-45bf-8afe-00de36975748"), "Parker", "Peter", new DateTime(1969, 5, 1)){ Username = "Spiderman", Descr = "Not a menthol fan" },
            new User(new Guid("ec401dbe-56f4-4b81-8c2a-0b84873dfdb2"), "Kent", "Clark", new DateTime(1938, 6, 1)){ Username = "Superman", Descr = "Perhaps we know him, but I don't have my glasses" },
            new User(new Guid("5335b7a4-e4b3-4129-8c05-df50f0aad1e1"), "Wilson", "Wade", new DateTime(1991, 2, 4)){ Username = "Deadpool", Descr = "Well..." },
            new User(new Guid("27b3623a-8f75-40f3-9313-884ad7f7b50b"), "Allen", "Barry", new DateTime(1940, 1, 9)){ Username = "Flash", Descr = "Look, a... ! Too late..." }
        };

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(userList);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(Guid id)
        {
            var user = userList.FirstOrDefault(usr => usr.Id == id);
            if (user != null && user.Id != Guid.Empty) return Ok(user);
            else return NotFound();
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] User _user)
        {
            _user.Id = Guid.NewGuid();
            userList.Add(_user);
            return Created("",_user.Id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] User _user)
        {
            var user = userList.FirstOrDefault(usr => usr.Id == id);
            if (user != null && user.Id != Guid.Empty)
            {
                user.Firstname = _user.Firstname;
                user.Gender = _user.Gender;
                user.Lastname = _user.Lastname;
                user.Username = _user.Username;
                return new StatusCodeResult(204);
            }
            else return NotFound();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var user = userList.FirstOrDefault(usr => usr.Id == id);
            userList.Remove(user);
            return new StatusCodeResult(204);
        }
    }

    public class User
    {
        public User()
        {
        }

        public User(Guid id, string lastname, string firstname, DateTime birthdate)
        {
            this.Id = id;
            this.Lastname = lastname;
            this.Firstname = firstname;
            this.Birthdate = birthdate;
        }

        public Guid Id { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string Username { get; set; }

        public DateTime Birthdate { get; set; }

        public Gender Gender { get; set; }

        public string Descr { get; set; } = string.Empty;
    }
    public enum Gender
    {
        /// <summary>
        /// The male gender.
        /// </summary>
        Male,

        /// <summary>
        /// The female gender.
        /// </summary>
        Female
    }
}
