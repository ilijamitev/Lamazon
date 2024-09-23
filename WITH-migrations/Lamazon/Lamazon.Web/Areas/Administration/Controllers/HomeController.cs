using Lamazon.Domain.Constants;
using Lamazon.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Roles.Admin)]
    public class HomeController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public HomeController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
            PageName = "Dashboard";
        }

        public IActionResult Index()
        {
            var dashboardViewModel = _dashboardService.GetDashboardData();
            return View(dashboardViewModel);
        }
    }
}
