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

    [HttpGet("user/{id}")]
    public ViewResult View(long id)
    {
        var user = _userService.GetById(id);
        if(user != null)
        {
            var userListItem = new UserListItemViewModel
            {
                Id = user.Id,
                Forename = user.Forename,
                Surname = user.Surname,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive
            };
            return View(userListItem);
        }
        return View(null);
    }

    [HttpGet("edit/{Id}")]
    public ViewResult Edit(long Id)
    {
        var user = _userService.GetById(Id);
        if (user != null)
        {
            var userListItem = new UserListItemViewModel
            {
                Id = user.Id,
                Forename = user.Forename,
                Surname = user.Surname,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive
            };
            return View(userListItem);
        }
        return View(null);
    }

    [HttpPost("edit/{id}")]
    public IActionResult Edit(UserListItemViewModel userModel)
    {
        var user = _userService.GetById(userModel.Id);
        if (user != null)
        {
            user.Forename = userModel.Forename;
            user.Surname = userModel.Surname;
            user.Email = userModel.Email;
            user.DateOfBirth = userModel.DateOfBirth;
            _userService.UpdateUser(user);
        }

        return RedirectToAction("All", "Users");
    }

    [HttpPost("delete")]
    public IActionResult Delete(long id)
    {
        var user = _userService.GetById(id);
        if (user != null)
            _userService.DeleteUser(user);

        return RedirectToAction("All", "Users");
    }
}
