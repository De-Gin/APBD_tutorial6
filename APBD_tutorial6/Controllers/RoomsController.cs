using APBD_tutorial6.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBD_tutorial6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController: ControllerBase
{
    // [HttpGet]
    // public IActionResult getAllRooms()
    // {
    //     return Ok(DataStore.Rooms);
    // }

    [HttpGet("/api/rooms/{id}")]
    public IActionResult getRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(x => x.Id == id);
        if (room == null)
            return NotFound();
        return Ok(room);
    }

    
    [HttpGet("/api/rooms/building/{buildingCode}")]
    public IActionResult getBuilding(int buildingCode)
    {
        var rooms = DataStore.Rooms.Where(x => x.BuildingCode.Equals(buildingCode));
        if (!rooms.Any())
            return NotFound();
        return Ok(rooms);
    }

    // [HttpGet("/api/rooms{?minCapacity=20&hasProjector=true&activeOnly=true}")]
    // public IActionResult getRoomsMinCapacity(int? minCapacity, bool? hasProjector, bool? activeOnly)
    // {
    //     var rooms = DataStore.Rooms.AsEnumerable();
    //     if (minCapacity.HasValue)
    //     {
    //         rooms = rooms.Where(x => x.Capacity >= minCapacity.Value);
    //     }
    //
    //     if (hasProjector.HasValue)
    //     {
    //         rooms = rooms.Where(x => x.HasProjector == hasProjector.Value);
    //     }
    //
    //     if (activeOnly.HasValue)
    //     {
    //         rooms = rooms.Where(x => x.IsActive == activeOnly.Value);
    //     }
    //     return Ok(rooms);
    // }
    
    [HttpGet]
    public IActionResult getAllRooms(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {
        var rooms = DataStore.Rooms.AsEnumerable();
        if (minCapacity.HasValue)
        {
            rooms = rooms.Where(x => x.Capacity >= minCapacity.Value);
        }

        if (hasProjector.HasValue)
        {
            rooms = rooms.Where(x => x.HasProjector == hasProjector.Value);
        }

        if (activeOnly.HasValue)
        {
            rooms = rooms.Where(x => x.IsActive == activeOnly.Value);
        }
        return Ok(rooms);
    }

    [HttpPost]
    public IActionResult createRoom([FromBody] Room room)
    {
        DataStore.Rooms.Add(room);
        return CreatedAtAction(nameof(getRoom), new { id = room.Id }, room);

    }

    [HttpPut("{id}")]
    public IActionResult updateRoom(int id, [FromBody] Room room)
    {
        var existing = DataStore.Rooms.FirstOrDefault(x => x.Id == id);
        if (existing == null)
            return NotFound();

        existing.Name = room.Name;
        existing.BuildingCode = room.BuildingCode;
        existing.Floor = room.Floor;
        existing.Capacity = room.Capacity;
        existing.HasProjector = room.HasProjector;
        existing.IsActive = room.IsActive;

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public IActionResult deleteRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(x => x.Id == id);
        if (room == null)
        {
            return NotFound();
        }

        if (DataStore.Reservations.Any(x => x.RoomId == room.Id))
        {
            return Conflict();
        }

        DataStore.Rooms.Remove(room);
        return NoContent();
    }
}

// Method Endpoint Description GET /api/rooms Returns all rooms.
//     GET /api/rooms/{id} Returns a single room by its identifier.
//     GET /api/rooms/building/{buildingCode} Returns rooms from the selected building.
//     The buildingCode parameter must be passed through the route.
//     GET /api/rooms?minCapacity=20&hasProjector=true&activeOnly=true Returns rooms filtered by query string parameters.
//     POST /api/rooms Adds a new room.
//     PUT /api/rooms/{id} Updates the full room data.
//     DELETE /api/rooms/{id} Deletes a room.