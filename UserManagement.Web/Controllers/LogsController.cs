using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Logs;

namespace UserManagement.Web.Controllers;

[Route("logs")]
public class LogsController : Controller
{
    private readonly IUserService _userService;
    public LogsController(IUserService userService) => _userService = userService;

    [HttpGet("all")]
    public ViewResult List()
    {
        var logs = _userService.GetLogs().Select(l => new LogListItemViewModel
        {
            Id = l.Id,
            UserId = l.UserId,
            Created = l.Created,
            Type = l.Type,
        });

        var model = new LogListViewModel
        {
            Logs = logs.ToList()
        };

        return View(model);
    }
}
