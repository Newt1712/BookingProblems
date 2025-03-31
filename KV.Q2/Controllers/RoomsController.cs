using KV.Q2.Entities;
using KV.Q2.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KV.Q2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public RoomsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }


        [HttpGet("check-availability")]
        public async Task<IActionResult> CheckAvailability(
             [FromQuery] int roomId,
             [FromQuery] DateTime date,
             [FromQuery] TimeSpan? checkIn = null,
             [FromQuery] TimeSpan? checkOut = null)
        {
            try
            {
                if (checkIn.HasValue && checkOut.HasValue)
                {
                    DateTime checkInDateTime = date.Date.Add(checkIn.Value);
                    DateTime checkOutDateTime = date.Date.Add(checkOut.Value);

                    bool available = await _bookingService.IsRoomAvailableByTimeRange(roomId, checkInDateTime, checkOutDateTime);
                    return Ok(available);
                }
                else
                {
                    bool available = await _bookingService.IsRoomAvailableByDay(roomId, date);
                    return Ok(new { roomId, date, available });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("book")]
        public async Task<IActionResult> Book(
            [FromQuery] int roomId, 
            [FromQuery] DateTime checkIn, 
            [FromQuery] DateTime checkOut)
        {
            bool success = await _bookingService.BookRoom(roomId, checkIn, checkOut);
            if (success)
                return Ok("Booking successful");
            return BadRequest("Room is not available");
        }


        [HttpDelete("cancel")]
        public async Task<IActionResult> Cancel(
            [FromQuery] int roomId, 
            [FromQuery] DateTime checkIn, 
            [FromQuery] DateTime checkOut)
        {
            bool success = await _bookingService.CancelBooking(roomId, checkIn, checkOut);
            if (success)
                return Ok("Booking canceled");
            return NotFound("Booking not found");
        }
    }
}
