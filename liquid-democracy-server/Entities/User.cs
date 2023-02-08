namespace Entities;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }
}
