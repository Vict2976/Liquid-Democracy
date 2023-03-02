public class Candidate
{
    [Key]
    public int CandidateId { get; set; }
    public string Name { get; set; }
    public int ElectionId { get; set; }

    public Election Election { get; set; }
    public ICollection<VoteUsedOn> DelegatedVotes { get; set; }
}