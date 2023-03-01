namespace Repository;

using Microsoft.EntityFrameworkCore;
using Security;
using Core;

public class UserRepository : IUserRepository
{
    ILiquidDemocracyContext _context;
    private IHasher _hasher;

    public UserRepository(ILiquidDemocracyContext context, IHasher hasher)
    {
        _context = context;
        _hasher = hasher;
    }

    public Response<User> Create(string username, string email, string password)
    {
        var existingUser = _context.Users.Any(u => u.UserName.Equals(username));

        if (existingUser){

            return new Response<User>
            {
                HTTPResponse = HTTPResponse.Conflict
            };
        }

        var user = new User
        {
            UserName = username,
            Email = email,
            Password = password,
            Votes = null,
        };

        _context.Users.Add(user);

        _context.SaveChanges();

        return new Response<User>
        {
            HTTPResponse = HTTPResponse.Created,
            Model = user
        };
        
    }

    public async Task<IEnumerable<User>?> ReadAllAsync(){
        var users = await _context.Users
            .ToListAsync();
        return users;
    }

    public Response<User> GetByUsername(string username){
        var user = _context.Users.Where(u => u.UserName == username).Select(u => u).First();

        if (user == null)
        {
            return new Response<User>
            {
                HTTPResponse = HTTPResponse.NotFound
            };
        }

        return new Response<User>
        {
            HTTPResponse = HTTPResponse.Success,
            Model = user
        };    
    }

    public async Task<IEnumerable<User>> GetAllDelegetasByElection(int electionId)
    {
        var votesInElection = await _context.Votes.Where(v => v.ElectionId == electionId).ToListAsync();
        
        var votesUsedOnList = new List<VoteUsedOn>();

        foreach(var vote in votesInElection){
            var votesUsedOn = await _context.VoteUsedOns.Where(VUO => VUO.VoteId == vote.VoteId && VUO.DelegateId != null).FirstOrDefaultAsync();
            votesUsedOnList.Add(votesUsedOn);
        }

        var delegateList = new List<User>();
        foreach(var voteUsedOn in votesUsedOnList){
            var delegatedUser = await _context.Users.Where(u => u.UserId == voteUsedOn.DelegateId).FirstOrDefaultAsync();
            delegateList.Add(delegatedUser);
        }

        return delegateList;
    }
}