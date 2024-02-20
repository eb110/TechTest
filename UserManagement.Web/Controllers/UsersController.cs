using System;
using System.Linq;
using UserManagement.Data.Entities;
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
    public ViewResult List(bool active, bool inactive)
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

        if (active) items = items.Where(x => x.IsActive);
        if (inactive) items = items.Where(y => !y.IsActive);

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
        users[users.Count - 1].AddLog(new Log { UserId = 1, Type = "Created", Created = DateTime.Now });
        _userService.UpdateUser(users[users.Count - 1]);
        return RedirectToAction("All", "Users");
    }

    [HttpGet("user/{id}")]
    public ViewResult View(long id)
    {
        var user = _userService.GetById(id);
        if (user != null)
        {
            user.AddLog(new Log { UserId = id, Type = "Viewed", Created = DateTime.Now });
            var userListItem = new UserListItemViewModel
            {
                Id = user.Id,
                Forename = user.Forename,
                Surname = user.Surname,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive
            };
            _userService.UpdateUser(user);
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
            user.AddLog(new Log { UserId = userModel.Id, Type = "Edited", Created = DateTime.Now });
            _userService.UpdateUser(user);
        }

        return RedirectToAction("All", "Users");
    }

    [HttpPost("delete")]
    public IActionResult Delete(long id)
    {
        var user = _userService.GetById(id);

        if (user != null)
        {
            user.AddLog(new Log { UserId = id, Type = "Edited", Created = DateTime.Now });
            _userService.UpdateUser(user);
            _userService.DeleteUser(user);
        }

        return RedirectToAction("All", "Users");
    }
}
