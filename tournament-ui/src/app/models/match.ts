import { Team } from "./team";

export class Match {
    id: number;
    tournamentId: number;
    sequenceNr : number;
    //team1: Team;
    //team2: Team;
    winnerId: number;
    loserId: number;
    winnerGoesToId: number;
    loserGoesToId: number;
    scoreTeam1: number;
    scoreTeam2: number;
    matchTeams: Team[];
}
