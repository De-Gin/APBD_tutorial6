using APBD_tutorial6.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_tutorial6.Controllers;



[ApiController]
[Route("api/[controller]")]
public class ReservationsController: ControllerBase
{
    
    [HttpGet("/api/reservations/{id}")]
    public IActionResult getReservation(int id)
    {
        var reservation = DataStore.Reservations.Any(r => r.Id == id);
        if (reservation == null)
        {
            return NotFound();
        }
        return Ok(reservation);
    }
    
    [HttpGet]
    public IActionResult getReservations(DateOnly? date, string? status, int? roomId)
    {
        var reservations = DataStore.Reservations.AsEnumerable();

        if (date.HasValue)
            reservations = reservations.Where(x => x.Date == date.Value);

        if (status != null)
            reservations = reservations.Where(x => x.Status == status);

        if (roomId.HasValue)
            reservations = reservations.Where(x => x.RoomId == roomId.Value);

        return Ok(reservations);
    }
    
    [HttpPost]
    public IActionResult createReservation([FromBody] Reservation reservation)
    {
        var room = DataStore.Rooms.FirstOrDefault(x => x.Id == reservation.RoomId);
        if (room == null)
            return NotFound("Room not found");

        if (!room.IsActive)
            return BadRequest("Room is not active");

        bool overlap = DataStore.Reservations.Any(x =>
            x.RoomId == reservation.RoomId &&
            x.Date == reservation.Date &&
            x.StartTime < reservation.EndTime &&
            x.EndTime > reservation.StartTime);

        if (overlap)
            return Conflict("Reservation overlaps with existing one");

        reservation.Id = DataStore.Reservations.Max(x => x.Id) + 1;
        DataStore.Reservations.Add(reservation);
        return CreatedAtAction(nameof(getReservation), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public IActionResult updateReservation(int id, [FromBody] Reservation reservation)
    {
        var existing = DataStore.Reservations.FirstOrDefault(x => x.Id == id);
        if (existing == null)
            return NotFound();

        existing.RoomId = reservation.RoomId;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Date = reservation.Date;
        existing.StartTime = reservation.StartTime;
        existing.EndTime = reservation.EndTime;
        existing.Status = reservation.Status;

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public IActionResult deleteReservation(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(x => x.Id == id);
        if (reservation == null)
            return NotFound();

        DataStore.Reservations.Remove(reservation);
        return NoContent();
    }
}



// Method Endpoint Description GET /api/reservations Returns all reservations.
//     GET /api/reservations/{id} Returns a single reservation.
//     GET /api/reservations?date=2026-05-10&status=confirmed&roomId=2 Returns reservations filtered by query string parameters.
//     POST /api/reservations Creates a new reservation.
//     PUT /api/reservations/{id} Updates an existing reservation.
//     DELETE /api/reservations/{id} Deletes a reservation.
//     Most important. The task must include different ways of passing data: id and buildingCode from the route,
//         filters from the query string, and object data from the request body in JSON format.