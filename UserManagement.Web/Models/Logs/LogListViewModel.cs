using System;

namespace UserManagement.Web.Models.Logs;

public class LogListViewModel
{
    public List<LogListItemViewModel> Logs { get; set; } = new();
}

public class LogListItemViewModel
{
    public long Id { get; set; }
    public string Type { get; set; } = default!;
    public long UserId { get; set; }
    public DateTime Created { get; set; }
}
