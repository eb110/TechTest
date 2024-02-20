using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
namespace UserManagement.Data.Entities;
public class Log
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Type { get; set; } = default!;
    public long UserId { get; set; }
    public DateTime Created { get; set; }
}
