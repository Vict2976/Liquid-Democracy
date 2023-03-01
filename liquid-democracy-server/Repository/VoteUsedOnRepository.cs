using Microsoft.EntityFrameworkCore;

namespace Repository;

public class VoteUsedOnRepository: IVoteUsedOnRepository
{
    ILiquidDemocracyContext _context;

    public VoteUsedOnRepository(ILiquidDemocracyContext context)
    {
        _context = context;
    }

    public async Task<VoteUsedOn?> CreateVoteUsedOn(int voteId, int? candidateId, int? delegateId)
    {
        var voteUsedOn = new VoteUsedOn()
        {
            VoteId = voteId,
            CandidateId = candidateId,
            DelegateId = delegateId
        };

        var response = _context.VoteUsedOns.Add(voteUsedOn);
        
        await _context.SaveChangesAsync();
        
        return voteUsedOn;
    }
}