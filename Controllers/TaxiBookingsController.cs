using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TaxiBookingAPI.Models;

namespace TaxiBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiBookingsController : ControllerBase
    {
        private readonly TaxiBookingContext _context;
        private readonly ILogger _logger;
        public TaxiBookingsController(TaxiBookingContext context, ILogger<TaxiBookingsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<ActionResult> GetCurrentLocation()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://api.freegeoip.app/json/?apikey=7322ce70-2741-11ec-a395-7f2b5c241db6"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation("Json Result" + apiResponse);
                        // JsonConvert.DeserializeObject<currentlocation>(apiResponse);
                        currentlocation locationinfo = JsonConvert.DeserializeObject<currentlocation>(apiResponse);
                        //TaxiBooking taxi = new TaxiBooking();
                        //taxi.Current_Location_Latitude = locationinfo.latitude;
                        //taxi.Current_Location_Longitude = locationinfo.longitude;
                        return Ok(locationinfo);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not locate the location" + ex.ToString());
                return null;
            }

        }
        // GET: api/TaxiBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxiBooking>>> GetTaxiBookingItems()
        {
            return await _context.TaxiBookingItems.ToListAsync();
        }

        // GET: api/TaxiBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxiBooking>> GetTaxiBooking(int id)
        {
            var taxiBooking = await _context.TaxiBookingItems.FindAsync(id);

            if (taxiBooking == null)
            {
                return NotFound();
            }

            return taxiBooking;
        }

        // PUT: api/TaxiBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxiBooking(int id, TaxiBooking taxiBooking)
        {
            if (id != taxiBooking.Id)
            {
                return BadRequest();
            }

            _context.Entry(taxiBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxiBookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaxiBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaxiBooking>> PostTaxiBooking(TaxiBooking taxiBooking)
        {
            try
            {
                currentlocation loca = new currentlocation();
                _logger.LogInformation("Lattidue" + loca.latitude);
                _logger.LogInformation("longitude" + loca.longitude);
                Console.WriteLine("Lattidue" + loca.latitude);
                Console.WriteLine("Lattidue" + loca.longitude);
                taxiBooking.Current_Location_Latitude = loca.latitude;
                taxiBooking.Current_Location_Longitude = loca.longitude;
                _context.TaxiBookingItems.Add(taxiBooking);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTaxiBooking", new { id = taxiBooking.Id }, taxiBooking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }

        // DELETE: api/TaxiBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxiBooking(int id)
        {
            var taxiBooking = await _context.TaxiBookingItems.FindAsync(id);
            if (taxiBooking == null)
            {
                return NotFound();
            }

            _context.TaxiBookingItems.Remove(taxiBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaxiBookingExists(int id)
        {
            return _context.TaxiBookingItems.Any(e => e.Id == id);
        }
    }
}
