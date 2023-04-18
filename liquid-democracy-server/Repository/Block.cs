namespace Repository;

using System.ComponentModel.DataAnnotations;

public class Block
{
    [Key]
    public int BlockId { get; set; }
    public int ElectionId { get; set; }
    public string? BlockHash { get; set; }

    public Election Election { get; set; }

}