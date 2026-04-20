# APBD Tutorial 6 - ASP.NET Core Web API

A simple in-memory Web API for managing training center rooms and reservations.

## Running the App
dotnet run

## Endpoints

### Rooms
- `GET /api/rooms` — get all rooms (supports `?minCapacity=`, `?hasProjector=`, `?activeOnly=`)
- `GET /api/rooms/{id}` — get room by id
- `GET /api/rooms/building/{buildingCode}` — get rooms by building
- `POST /api/rooms` — create a room
- `PUT /api/rooms/{id}` — update a room
- `DELETE /api/rooms/{id}` — delete a room

### Reservations
- `GET /api/reservations` — get all reservations (supports `?date=`, `?status=`, `?roomId=`)
- `GET /api/reservations/{id}` — get reservation by id
- `POST /api/reservations` — create a reservation
- `PUT /api/reservations/{id}` — update a reservation
- `DELETE /api/reservations/{id}` — delete a reservation
