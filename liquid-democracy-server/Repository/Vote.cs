using System.ComponentModel.DataAnnotations;

namespace Repository;

public class Vote
{
    [Key]
    public int VoteId { get; set; }
    public int BelongsToId { get; set; }
    public int ElectionId { get; set; }

    public User User { get; set; }
    public Election Election { get; set; }
    public ICollection<VoteUsedOn> DelegatedVotes { get; set; }

}