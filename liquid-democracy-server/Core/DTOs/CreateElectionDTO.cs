
public record CreateElectionDTO{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<string> Candidates { get; set; }

}