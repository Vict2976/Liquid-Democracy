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
            DelegateId = null
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

        public async Task<VoteUsedOn?> CreateVoteUsedOnForDelegate(int voteId, int? delegateId, int electionId)
    {
        
        var voteUsedOn = new VoteUsedOn()
        {
            VoteId = voteId,
            CandidateId = null,
            DelegateId = delegateId
        };
        
        var existingvoteUsedOn = await findNextInTree(voteUsedOn, electionId);

        while (existingvoteUsedOn.DelegateId != null){
            existingvoteUsedOn = await findNextInTree(existingvoteUsedOn, electionId);
        }

        var candidate = await _context.Candidates.Where(c => c.CandidateId == existingvoteUsedOn.CandidateId).FirstOrDefaultAsync();

        if (candidate != null){

            candidate.RecievedVotes ++;
            _context.Candidates.Update(candidate);
        }

        var response = _context.VoteUsedOns.Add(voteUsedOn);
        
        await _context.SaveChangesAsync();
        
        return voteUsedOn;
    }

    private async Task<VoteUsedOn> findNextInTree(VoteUsedOn voteUsedOn, int electionId)
    {
        var vote = await _context.Votes.Where(v => v.BelongsToId == voteUsedOn.DelegateId && v.ElectionId == electionId).FirstAsync();
        var delegatedsVoteUsedOn = await _context.VoteUsedOns.Where(VUO => VUO.VoteId == vote.VoteId).FirstAsync();
        return delegatedsVoteUsedOn;
    }
}