using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanYourTrip_ClassLibrary.Classes;

namespace PlanYourTrip_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripPlanController : ControllerBase
    {

        private readonly DataContext _context;

        public TripPlanController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TripPlans>>> GetAllPlans()
        {
            return Ok(await _context.TripPlans.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TripPlans>> GetTripPlan(int id)
        {
            var tripPlan = await _context.TripPlans.FindAsync(id);
            if(tripPlan == null)
            {
                return BadRequest("Nie odnaleziono planu");
            }

            return Ok(tripPlan);
        }

        [HttpGet]
        [Route("MyPlans/{userId}")]
        public async Task<ActionResult<TripPlans>> GetUserTripPlans(int userId)
        {
            var tripPlans = await _context.TripPlans
                .FromSqlRaw($"SELECT * FROM dbo.TripPlans WHERE AutorId = {userId}")
                .ToListAsync();
            if (tripPlans == null)
            {
                return BadRequest("Nie odnaleziono planu");
            }

            return Ok(tripPlans);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTripPlan(int id, TripPlans request)
        {
            if (id != request.TripPlanId)
            {
                return BadRequest();
            }

            var tripPlan = await _context.TripPlans.FindAsync(id);
            if (tripPlan == null)
            {
                return BadRequest("Nie znaleziono planu");
            }

            tripPlan.Nazwa = request.Nazwa;
            tripPlan.Opis = request.Opis;
            tripPlan.PunktyJSON = request.PunktyJSON;
            tripPlan.DataUtworzenia = request.DataUtworzenia;
            tripPlan.OstatniaAktualizacja = request.OstatniaAktualizacja;
            tripPlan.AutorId = request.AutorId;
            tripPlan.Publiczny = request.Publiczny;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TripPlans>> CreateTripPlan(TripPlans plan)
        {
            try
            {
                _context.TripPlans.Add(plan);
                return Ok(await _context.SaveChangesAsync());
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTripPlan(int id)
        {
            var tripPlan = await _context.TripPlans.FindAsync(id);
            if (tripPlan == null)
            {
                return BadRequest("Nie znaleziono planu");
            }

            try
            {
                _context.TripPlans.Remove(tripPlan);
                return Ok(await _context.SaveChangesAsync());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
