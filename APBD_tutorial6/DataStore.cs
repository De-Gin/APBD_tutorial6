using APBD_tutorial6.Models;

namespace APBD_tutorial6;

public static class DataStore
{
    public static List<Room> Rooms { get; } = new List<Room>
    {
        new Room { Id = 1, Name = "Lab 101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "Lab 202", BuildingCode = "A", Floor = 2, Capacity = 30, HasProjector = false, IsActive = true },
        new Room { Id = 3, Name = "Conference C", BuildingCode = "B", Floor = 1, Capacity = 15, HasProjector = true, IsActive = true },
        new Room { Id = 4, Name = "Workshop D", BuildingCode = "B", Floor = 3, Capacity = 25, HasProjector = false, IsActive = false },
        new Room { Id = 5, Name = "Seminar E", BuildingCode = "C", Floor = 2, Capacity = 40, HasProjector = true, IsActive = true },
    };

    public static List<Reservation> Reservations { get; } = new List<Reservation>
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Jan Kowalski", Topic = "C# Basics", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 0), Status = "planned" },
        new Reservation { Id = 2, RoomId = 2, OrganizerName = "Anna Nowak", Topic = "REST APIs", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(12, 0), EndTime = new TimeOnly(14, 0), Status = "confirmed" },
        new Reservation { Id = 3, RoomId = 1, OrganizerName = "Piotr Wiśniewski", Topic = "Git Workshop", Date = new DateOnly(2026, 5, 11), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 0), Status = "confirmed" },
        new Reservation { Id = 4, RoomId = 3, OrganizerName = "Maria Wójcik", Topic = "Agile Methods", Date = new DateOnly(2026, 5, 12), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(16, 0), Status = "cancelled" },
        new Reservation { Id = 5, RoomId = 5, OrganizerName = "Tomasz Zając", Topic = "Docker Intro", Date = new DateOnly(2026, 5, 13), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 30), Status = "planned" },
    };
}