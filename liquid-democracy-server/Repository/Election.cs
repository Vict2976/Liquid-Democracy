namespace Repository;

using System.ComponentModel.DataAnnotations;

public class Election
{
    [Key]
    public int ElectionId { get; set; }
    public string? Name { get; set; }
    public int AmountOfVotes { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Candidate>? Candidates { get; set; }
    public List<Votings> Votings{get; set;}
}
