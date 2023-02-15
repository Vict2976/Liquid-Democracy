using System.ComponentModel.DataAnnotations;

namespace Repository;

public class Votings
{
    [Key]
    public int VoteId { get; set; }
    public int AmountOfVotes { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int ElectionId { get; set; }
    public Election? Election { get; set; }
    public int CandidateId { get; set; }
    public Candidate? Candidate { get; set; }

}