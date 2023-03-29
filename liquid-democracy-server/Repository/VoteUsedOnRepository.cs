using Microsoft.EntityFrameworkCore;

namespace Repository;

public class VoteUsedOnRepository: IVoteUsedOnRepository
{
    ILiquidDemocracyContext _context;

    public VoteUsedOnRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<VoteUsedOn?> CreateVoteUsedOnForCandidate(int voteId, int? candidateId)
    {

        var voteUsedOn = new VoteUsedOn()
        {
            VoteId = voteId,
            CandidateId = candidateId,
        };

        var candidate = await _context.Candidates.Where(c => c.CandidateId == voteUsedOn.CandidateId).FirstOrDefaultAsync();

        if (candidate != null){

            candidate.RecievedVotes ++;
            _context.Candidates.Update(candidate);
        }

        var response = _context.VoteUsedOns.Add(voteUsedOn);
        
        await _context.SaveChangesAsync();
        
        return voteUsedOn;
    }
}