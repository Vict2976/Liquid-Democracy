namespace Repository;

using System.ComponentModel.DataAnnotations;

public class Candidate
{
    [Key]
    public int CandidateId { get; set; }
    public string Name { get; set; }
    public int ElectionId { get; set; }    
    public string RootHash { get; set; }
    public Election Election { get; set; }
}