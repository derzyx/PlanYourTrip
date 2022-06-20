using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanYourTrip_ClassLibrary.Classes;
using PlanYourTrip_ClassLibrary.ClassesDTO;

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

        // Plans
        // Plans/{id}
        // Plans/{userId}
        // Plans/{userId}/Public
        // Plans/Filter
        // Plans/Latest/{quantity}
        // Plans/Latest/{userId}/{quantity}
        // Plans/SubsLatest
        //
        // Put {id}
        // Post
        // Delete {id}


        [HttpGet]
        [Route("Plans")]
        public async Task<ActionResult<List<TripPlans>>> GetAllPlans()
        {
            return Ok(await _context.TripPlans.ToListAsync());
        }

        [HttpGet]
        [Route("Plans/{id}")]
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
        [Route("Plans/User/{userId}")]
        public async Task<ActionResult<List<TripPlans>>> GetUserTripPlans(int userId)
        {
            var tripPlans = await _context.TripPlans
                .FromSqlRaw($"SELECT * FROM dbo.TripPlans WHERE AutorId = {userId} ORDER BY DataUtworzenia DESC")
                .ToListAsync();
            if (tripPlans == null)
            {
                return BadRequest("Nie odnaleziono planów ");
            }

            return Ok(tripPlans);
        }

        [HttpGet]
        [Route("Plans/User/{userId}/Public")]
        public async Task<ActionResult<List<TripPlans>>> GetUserPublicTripPlans(int userId)
        {
            var tripPlans = await _context.TripPlans
                .FromSqlRaw($"SELECT * FROM dbo.TripPlans WHERE Publiczny = 1 AND AutorId = {userId}  ORDER BY DataUtworzenia DESC")
                .ToListAsync();
            if (tripPlans == null)
            {
                return BadRequest("Nie odnaleziono planów ");
            }

            return Ok(tripPlans);
        }

        [HttpPost]
        [Route("Plans/Filter")]
        public async Task<ActionResult<List<TripPlans>>> GetFilteredPlans(PlansFilterDTO filters)
        {
            List<TripPlans> tripPlans;
            string dateMinParsed = "";
            string dateMaxParsed = "";

            if (!string.IsNullOrEmpty(filters.DateMin))
                dateMinParsed = DateTime.Parse(filters.DateMin).ToString("O");

            if (!string.IsNullOrEmpty(filters.DateMax))
                dateMaxParsed = DateTime.Parse(filters.DateMax).ToString("O");

            string sortType = (filters.SortType == "desc") ? "DESC" : "ASC";


            if (string.IsNullOrEmpty(dateMinParsed) && string.IsNullOrEmpty(dateMaxParsed))
            {
                tripPlans = await _context.TripPlans
                    .FromSqlRaw($"SELECT * FROM TripPlans WHERE Publiczny = 1 ORDER BY DataUtworzenia {sortType}")
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(dateMinParsed) && string.IsNullOrEmpty(dateMaxParsed))
            {
                tripPlans = await _context.TripPlans
                    .FromSqlRaw($"SELECT * FROM TripPlans WHERE Publiczny = 1 AND DataUtworzenia >= '{dateMinParsed}' ORDER BY DataUtworzenia {sortType}")
                    .ToListAsync();
            }
            else if (string.IsNullOrEmpty(dateMinParsed) && !string.IsNullOrEmpty(dateMaxParsed))
            {
                tripPlans = await _context.TripPlans
                    .FromSqlRaw($"SELECT * FROM TripPlans WHERE Publiczny = 1 AND DataUtworzenia <= '{dateMaxParsed}' ORDER BY DataUtworzenia {sortType}")
                    .ToListAsync();
            }
            else
            {
                tripPlans = await _context.TripPlans
                    .FromSqlRaw($"SELECT * FROM TripPlans WHERE Publiczny = 1 AND DataUtworzenia BETWEEN '{dateMinParsed}' AND '{dateMaxParsed}' ORDER BY DataUtworzenia {sortType}")
                    .ToListAsync();
            }

            if (tripPlans == null)
            {
                return BadRequest("Nie znaleziono planów");
            }

            return Ok(tripPlans);
        }

        [HttpGet]
        [Route("Plans/Latest/{quantity}")]
        public async Task<ActionResult<List<TripPlans>>> GetTopTripPlans(int quantity)
        {
            var tripPlans = await _context.TripPlans
                .FromSqlRaw($"SELECT TOP {quantity} * FROM TripPlans WHERE Publiczny = 1 ORDER BY DataUtworzenia DESC")
                .ToListAsync();

            if (tripPlans == null)
            {
                return BadRequest("Brak planów");
            }

            return Ok(tripPlans);
        }

        [HttpGet]
        [Route("LatestsPlans/{userId}/{quantity}")]
        public async Task<ActionResult<List<TripPlans>>> GetUserTopTripPlans(int userId, int quantity)
        {
            var tripPlans = await _context.TripPlans
                .FromSqlRaw($"SELECT TOP {quantity} * FROM TripPlans WHERE Publiczny = 1 AND AutorId = {userId} ORDER BY DataUtworzenia DESC")
                .ToListAsync();

            if (tripPlans == null)
            {
                return BadRequest("Brak planów");
            }

            return Ok(tripPlans);
        }

        [HttpPost]
        [Route("Plans/SubsLatest")]
        public async Task<ActionResult<List<TripPlans>>> GetSubsLatestPlans(List<int> userIds)
        {
            int quantity = userIds.Last<int>();
            List<TripPlans> subLatestPlans = new List<TripPlans>();

            for(int i = 0; i < userIds.Count-1; i++)
            {
                subLatestPlans.AddRange(
                    await _context.TripPlans
                    .FromSqlRaw($"SELECT TOP {quantity} * FROM TripPlans WHERE Publiczny = 1 AND AutorId = {userIds[i]} ORDER BY DataUtworzenia DESC")
                    .ToListAsync());
            }

            return(subLatestPlans);
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
                return Ok(await _context.SaveChangesAsync());
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
