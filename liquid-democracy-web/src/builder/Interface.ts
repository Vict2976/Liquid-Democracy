export interface User
{
    userId : number,
    userName : string,
    //votings: number[],
    //elections: number[]
}

export interface Election
{
    electionId: number,
    name: string,
    description: string,
    createdDate: string,
    isEnded: boolean,
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

export interface CreateCandidate
{
    name: string
} 

export interface Votings
{
    voteId: number,
    amountOfVotes: number,
    userId: number,
    electionId: number,
    candidateId: number
} 

 