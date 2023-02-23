namespace Repository;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public ICollection<Vote> Votes { get; set; }
    public ICollection<VoteUsedOn> DelegatedVotes { get; set; }
}