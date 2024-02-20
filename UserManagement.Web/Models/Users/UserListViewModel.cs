namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel : UserModel
{
    public long Id { get; set; }
    public bool IsActive { get; set; }
}
