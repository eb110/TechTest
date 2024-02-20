using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet("all")]
    public ViewResult List()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            DateOfBirth = p.DateOfBirth,
            IsActive = p.IsActive
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet("add")]
    public ViewResult Add()
    {
        return View();
    }

    [HttpPost("add")]
    public IActionResult Add(UserModel viewModel)
    {
        var user = new User { Forename = viewModel.Forename, Surname = viewModel.Surname, Email = viewModel.Email, DateOfBirth = viewModel.DateOfBirth };
        _userService.Add(user);
        var users = _userService.GetAll().ToList();
        return RedirectToAction("All", "Users");
    }
}
