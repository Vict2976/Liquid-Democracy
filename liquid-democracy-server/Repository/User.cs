namespace Repository;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int UserId { get; set; }

    public string ProivderId {get; set;}
    public string? Email { get; set; }
    public DateTime? SesseionExpires {get; set;}
    public ICollection<Vote> Votes { get; set; }
}