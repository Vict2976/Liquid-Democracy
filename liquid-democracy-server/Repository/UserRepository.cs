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

        var listOfDelegates = new List<User>();

        foreach(var vote in votesInElection){
            var user = await _context.Users.Where(u => u.UserId == vote.BelongsToId).FirstOrDefaultAsync();
            listOfDelegates.Add(user);
        }
        return listOfDelegates;
    }

    public Response<User> AddMidIDSession(int userId){
        var user = _context.Users.Where(u => u.UserId == userId).Select(u => u).First();

        if (user == null)
        {
            return new Response<User>
            {
                HTTPResponse = HTTPResponse.NotFound
            };
        }

        user.LoggedInWithNemId = DateTime.Now;
        _context.Users.Update(user);
        _context.SaveChangesAsync();

        return new Response<User>
        {
            HTTPResponse = HTTPResponse.Success,
            Model = user
        };        
    }

    public async Task<bool> CheckMitIDTimeStamp(int userId){
        var user = _context.Users.Where(u => u.UserId == userId).Select(u => u).First();
        if (user == null){
            throw new Exception();
        }
        return IsLoggedInWithinThiryMinues(user.LoggedInWithNemId);
    }

    private bool IsLoggedInWithinThiryMinues(DateTime? mitIdLoggedInAt)
    {
        if (mitIdLoggedInAt != null)
        {
            var timeSinceLoggedIn = DateTime.Now.Subtract((DateTime)mitIdLoggedInAt);
            if (timeSinceLoggedIn.TotalMinutes > 30){
                return false;
            }else{
                return true;
            }
        }    
        return false;
    }
}