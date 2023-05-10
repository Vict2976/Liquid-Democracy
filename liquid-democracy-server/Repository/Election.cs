namespace Repository;

using System.ComponentModel.DataAnnotations;

public class Election
{
    [Key]
    public int ElectionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsEnded {get; set;}
    public DateTime CreatedDate { get; set; }
    public string? RootHash { get; set; }
    public string? LatestHash { get; set;}

    public ICollection<Candidate> Candidates { get; set; }

}
