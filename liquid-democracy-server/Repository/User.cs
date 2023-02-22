namespace Repository;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string? Name { get; set; }
    public List<Votings>? Votings { get; set; }
    public List<Election>? Elections { get; set; }
}
