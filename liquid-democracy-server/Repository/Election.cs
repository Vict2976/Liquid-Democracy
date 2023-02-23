namespace Repository;

using System.ComponentModel.DataAnnotations;

public class Election
{
    [Key]
    public int ElectionID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<Candidate> Candidates { get; set; }
    public ICollection<Vote> Votes { get; set; }
}
