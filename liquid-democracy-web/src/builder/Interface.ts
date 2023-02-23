export interface User
{
    userId : number,
    name : string,
    votings: number[],
    elections: number[]
}

export interface Election
{
    electionId: number,
    name: string,
    amountOfVotes: number,
    userId: number,
    candidates: number[],
    votings: number[]
} 

export interface Candidate
{
    candidateId: number,
    name: string,
    recievedVotes: number,
    electionId: number,
} 

export interface Votings
{
    voteId: number,
    amountOfVotes: number,
    userId: number,
    electionId: number,
    candidateId: number
} 

 