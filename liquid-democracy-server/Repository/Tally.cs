namespace Repository;

using System.ComponentModel.DataAnnotations;

public class Tally
{
    [Key]
    public int TallyId { get; set; }
    public int candidateId { get; set; }

}
