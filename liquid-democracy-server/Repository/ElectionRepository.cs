using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ElectionRepository : IElectionRepository
{
    ILiquidDemocracyContext _context;

    public ElectionRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<Election?> CreateAsync(string name, int userId, List<Candidate> candidates){

        //var userExist = _context.Users.Any(u=> u.UserId == userId);
        /*if(userExist) {
            var user = _context.Users.Where(u => u.UserId == userId).Select(u => u).First();
        */
        var election = new Election
            {
                Name = name,
                AmountOfVotes = 0,
                UserId = userId, 
                //User = user,
                Candidates = null,
                Votings = null
            };
        
        _context.Elections.Add(election);

        await _context.SaveChangesAsync();

        return election;


    }

    public async Task<IEnumerable<Election>?> ReadAllAsync(){
        var elections = await _context.Elections
            .ToListAsync();
        return elections;
    }



    public async Task<Election?> GetElectionByIDAsync(int electionId){
        var election = await _context.Elections.Where(c => c.ElectionId == electionId).Select(c=> c).FirstAsync();

        return election;
    }
}