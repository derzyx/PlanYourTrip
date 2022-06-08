using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;
using System.Linq.Expressions;

namespace PlanYourTrip_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        //Get/All
        //Get/ById/{userId}
        //Get/ByEmail/{email}
        //Get/PublicData/One/{userId}
        //Get/PublicData/Many
        //Get/Nicks
        //
        //Add
        //Login
        //Update/{userId}
        //PatchPassword/{userId}
        //Remove/{userId}

        [HttpGet]
        [Route("Get/All")]
        public async Task<ActionResult<List<Users>>> GetAllUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet]
        [Route("Get/ById/{userId}")]
        public async Task<ActionResult<Users>> GetUserByID(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("Nie znaleziono użytkownika");
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("Get/ByEmail/{email}")]
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

        [HttpGet]
        [Route("Get/PublicData/One/{userId}")]
        public async Task<ActionResult<UserPublicDataDTO>> GetUserPublicData(int userId)
        {
            Users userData = await _context.Users.FindAsync(userId);
            if (userData == null)
            {
                return BadRequest("Nie znaleziono użytkownika");
            }


            return Ok(new UserPublicDataDTO
            {
                Id = userData.Id,
                Nick = userData.Nick,
                Imie = userData.Imie,
                Nazwisko = userData.Nazwisko,
                Opis = userData.Opis
            });
        }

        [HttpPost]
        [Route("Get/PublicData/Many")]
        public async Task<ActionResult<List<UserPublicDataDTO>>> ReturnUsersPublicData(List<int> usersIds)
        {
            List<UserPublicDataDTO> usersData = new List<UserPublicDataDTO>();

            if (usersIds == null)
            {
                return BadRequest();
            }

            foreach (int id in usersIds)
            {
                Users userData = await _context.Users.FindAsync(id);
                usersData.Add(new UserPublicDataDTO
                {
                    Id = userData.Id,
                    Nick = userData.Nick,
                    Imie = userData.Imie,
                    Nazwisko = userData.Nazwisko,
                    Opis = userData.Opis
                });
            }

            return usersData;
        }

        [HttpPost]
        [Route("Get/Nicks")]
        public async Task<ActionResult<List<string>>> ReturnUsersNicks(List<int> usersIds)
        {
            List<string> userNicks = new List<string>();

            if (usersIds == null)
            {
                return BadRequest();
            }

            foreach (int id in usersIds)
            {
                Users user = await _context.Users.FindAsync(id);
                userNicks.Add(user.Nick);
            }

            return userNicks;
        }



        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            string errorString = "";

            List<Users> sameNickUsers = await _context.Users
                .FromSqlRaw($"SELECT * FROM dbo.Users WHERE Nick = '{user.Nick}'")
                .ToListAsync();

            List<Users> sameEmailUsers = await _context.Users
                .FromSqlRaw($"SELECT * FROM dbo.Users WHERE Email = '{user.Email}'")
                .ToListAsync();

            errorString += (sameNickUsers.Count == 0) ? "" : "Ten nick jest już zajęty! ";
            errorString += (sameEmailUsers.Count == 0) ? "" : "Ktoś już korzysta z tego emaila! ";

            if(errorString.Length > 0)
            {
                return StatusCode(406, errorString);
            }
            else
            {
                try
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return Ok("Pomyślnie dodano użytkownika");
                }
                catch (Exception)
                {
                    return BadRequest("Coś poszło nie tak <br/>");
                }

            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<Users>> LoginUser(UsersDTO userToLog)
        {
            var user = await _context.Users
                .FromSqlRaw($"SELECT * FROM dbo.Users WHERE Email = '{userToLog.Email}'")
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound("Niepoprawne dane logowania!");
            }
            else
            {
                if(user.Haslo == userToLog.Haslo)
                {
                    return Ok("Pomyślnie zalogowano");
                }
                else
                {
                    return BadRequest("Niepoprawne dane logowania!");
                }
            }
        }

        [HttpPut]
        [Route("Update/{userId}")]
        public async Task<ActionResult<Users>> UpdateUser(int userId, Users request)
        {
            string errorString = "";
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return BadRequest("Nie znaleziono użytkownika <br/>");
            }

            if(user.Nick != request.Nick)
            {
                List<Users> sameNickUsers = await _context.Users
                    .FromSqlRaw($"SELECT * FROM dbo.Users WHERE Nick = '{request.Nick}'")
                    .ToListAsync();

                errorString += (sameNickUsers.Count == 0) ? "" : "Ten nick jest już zajęty! <br/>";
            }

            if(user.Email != request.Email)
            {
                List<Users> sameEmailUsers = await _context.Users
                    .FromSqlRaw($"SELECT * FROM dbo.Users WHERE Email = '{request.Email}'")
                    .ToListAsync();

                errorString += (sameEmailUsers.Count == 0) ? "" : "Ktoś już korzysta z tego emaila! <br/>";
            }

            if (errorString.Length > 0)
            {
                return StatusCode(406, errorString);
            }
            else
            {
                user.Nick = request.Nick;
                user.Imie = request.Imie;
                user.Nazwisko = request.Nazwisko;
                user.Email = request.Email;
                user.Opis = request.Opis;
                user.IdAvatar = request.IdAvatar;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return BadRequest("Coś poszło nie tak <br/>");
                    throw;
                }

                return Ok("Pomyślnie zaktualizowano użytkownika <br/>");
            }
        }

        [HttpPatch]
        [Route("PatchPassword/{userId}")]
        public async Task<ActionResult<Users>> UpdatePassword(int userId, [FromBody] PasswordDTO passwords)
        {
            if(passwords != null)
            {
                var user = await _context.Users.FindAsync(userId);


                // Validation
                string errorMessage = "";
                if(passwords.OldPassword != user.Haslo)
                {
                    errorMessage = "Stare hasło jest nieprawidłowe <br/>";
                }

                if (passwords.OldPassword == null || passwords.NewPassword == null || passwords.NewPasswordRepeat == null)
                {
                    errorMessage += "Nie wypełniono wymaganego pola <br/>";
                }

                if (passwords.NewPassword != passwords.NewPasswordRepeat)
                {
                    errorMessage += "Nowe hasła nie są takie same <br/>";
                }

                if (passwords.NewPassword.Length < 8 || passwords.NewPassword.Length > 30 ||
                    passwords.NewPasswordRepeat.Length < 8 || passwords.NewPasswordRepeat.Length > 30)
                {
                    errorMessage += "Hasło musi mieć od 8 do 30 znaków <br/>";
                }

                if (errorMessage.Length > 0)
                {
                    return BadRequest(errorMessage);
                }

                // Update
                user.Haslo = passwords.NewPassword;
                await _context.SaveChangesAsync();

                return Ok("Pomyślnie zmieniono hasło <br/>");
            }
            else
            {
                return BadRequest("Coś poszło nie tak <br/>");
            }
        }

        [HttpDelete]
        [Route("Remove/{userId}")]
        public async Task<ActionResult<Users>> RemoveUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("Nie znaleziono użytkownika");
            }
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Coś poszło nie tak <br/>");
            }

        }
    }
}
