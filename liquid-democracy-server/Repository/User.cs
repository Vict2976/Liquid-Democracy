namespace Repository;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public List<Votings>? Votings { get; set; }
    public List<Election>? Elections { get; set; }
}
