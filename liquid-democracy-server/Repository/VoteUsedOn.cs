using System.ComponentModel.DataAnnotations;

namespace Repository;

public class VoteUsedOn
{
    [Key]
    public int VoteUsedOnId { get; set; }
    public int VoteId { get; set; }
    public int? CandidateId { get; set; }
    public int? DelegateId { get; set; }

    public Vote Vote { get; set; }
    public Candidate? Candidate { get; set; }
    public User? Delegate { get; set; }

}