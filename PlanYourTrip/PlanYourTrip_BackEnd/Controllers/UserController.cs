using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanYourTrip_ClassLibrary.Classes;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("Nie znaleziono użytkownika");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<Users>>> AddUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Users>>> UpdateUser(Users request)
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

            return Ok(await _context.Users.ToListAsync());
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
