using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_room_management_C_.Models;

namespace project_room_management_C_.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly RoomManagementContext _context;

        public DashboardController(RoomManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var nextSevenDays = today.AddDays(7);

            var viewModel = new DashboardViewModel
            {
                // Room::count()
                TotalRooms = await _context.Rooms.CountAsync(),

                // Room::where('status', 'Đã thuê')->count()
                RentedRooms = await _context.Rooms.CountAsync(r => r.Status == "Đã thuê"),

                // Room::where('status', 'Trống')->count()
                EmptyRooms = await _context.Rooms.CountAsync(r => r.Status == "Trống"),

                // Tenant::count()
                TotalTenants = await _context.Tenants.CountAsync(),

                // Room::where('status', 'Trống')->get()
                AvailableRooms = await _context.Rooms
                    .Where(r => r.Status == "Trống")
                    .ToListAsync(),

                // Contract::where('status', 'Còn hạn')->whereBetween(...)
                ExpiringContracts = await _context.Contracts
                    .Where(c => c.Status == "Còn hạn"
                             && c.EndDay.HasValue
                             && c.EndDay.Value >= today
                             && c.EndDay.Value <= nextSevenDays)
                    .ToListAsync()
            };

            return View(viewModel);
        }
    }
}