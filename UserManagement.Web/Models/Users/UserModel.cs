using System;

namespace UserManagement.Web.Models.Users;

public class UserModel
{
    public string Forename { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
}
