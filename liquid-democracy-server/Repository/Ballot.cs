namespace Repository;

using System;
using System.ComponentModel.DataAnnotations;

public class Ballot
{
    [Key]
    public int BallotId { get; set; }
    public string? CandidateId { get; set; }
    public string? Uuid { get; set; }
    public string? TimeStamp { get; set; }
    public string? Nonce { get; set;}
    public string? DocumentId { get; set; }
    public string? RootHash { get; set; }
}