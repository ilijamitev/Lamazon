using Lamazon.Domain.Constants;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lamazon.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IRolesService _rolesService;

        public UsersController(IUsersService usersService, IRolesService rolesService)
        {
            _usersService = usersService;
            _rolesService = rolesService;

            PageName = "Users";
        }

        public IActionResult Index()
        {
            SetMetadata();
            return View();
        }

        [HttpPost]
        public JsonResult GetUsers(UsersDatatableRequestViewModel usersDatatableRequestViewModel)
        {
            var pagedResult = _usersService.GetFilteredUsers(usersDatatableRequestViewModel);
            return Json(pagedResult.ToTableData());
        }

        public IActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var userViewModel = _usersService.GetUserById(id.Value);
                SetMetadata(userViewModel);
                return View(userViewModel);
            }
            else
            {
                return new EmptyResult();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                _usersService.UpdateUser(userViewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                SetMetadata(userViewModel);
                return View();
            }
        }

        private void SetMetadata(UserViewModel userViewModel = null)
        {
            var roles = _rolesService.GetAllRoles();
            ViewBag.RoleKey = new SelectList(roles, "Key", "Name", userViewModel?.RoleKey);
        }
    }
}
