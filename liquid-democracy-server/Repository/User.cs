namespace Repository;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime? LoggedInWithNemId {get; set;}
    
    public ICollection<Vote> Votes { get; set; }
    public ICollection<VoteUsedOn> DelegatedVotes { get; set; }
}