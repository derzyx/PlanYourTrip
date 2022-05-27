using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;
//using System.Web.Http;

namespace PlanYourTrip_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<Users> users = new List<Users>
        {

        };

        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAllUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet]
        [Route("Id/{id}")]
        public async Task<ActionResult<Users>> GetUserByID(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("Nie znaleziono użytkownika");
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("Email/{email}")]
        public async Task<ActionResult<Users>> GetUserByEmail(string email)
        {
            var user = await _context.Users
                .FromSqlRaw($"SELECT * FROM dbo.Users WHERE Email = '{email}'")
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("Nie znaleziono użytkownika");
            }
            else
            {
                return Ok(user);
            } 
        }

        [HttpPost]
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Dodano użytkownika");
            //return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<Users>> LoginUser(UsersDTO userToLog)
        {
            Console.WriteLine(userToLog);
            var user = await _context.Users
                .FromSqlRaw($"SELECT * FROM dbo.Users WHERE Email = '{userToLog.Email}'")
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if(user.Haslo == userToLog.Haslo)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> UpdateUser(Users request)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
            {
                return BadRequest("Nie znaleziono użytkownika");
            }

            user.Nick = request.Nick;
            user.Imie = request.Imie;
            user.Nazwisko = request.Nazwisko;

            await _context.SaveChangesAsync();

            return Ok("Zaktualizowano użytkownika");
            //return Ok(await _context.Users.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> RemoveUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("Nie znaleziono użytkownika");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
    }
}
