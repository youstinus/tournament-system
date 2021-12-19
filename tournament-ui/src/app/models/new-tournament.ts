import { GameType } from "./game-type";
import { Team } from "./team";

export class NewTournament {
    id: number;
    title: string;
    date: Date;
    maxParticipantPerTeam: number;
    game: GameType;
    numberOfTeams: number;
    winnerTeamId: number;
}
