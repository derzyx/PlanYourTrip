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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTripPlan(int id, TripPlans request)
        {
            string tripToUpdate = request.ToString();
            TripPlans trip = new TripPlans { AutorId = 1, Nazwa = "aaa" };

            Console.WriteLine(tripToUpdate);
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
    }
}
