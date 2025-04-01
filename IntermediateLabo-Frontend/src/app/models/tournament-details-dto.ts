export interface TournamentDetailsDTO {
  id: number
  name: string;
  location?: string;
  currentPlayerAmount: number;
  minPlayerAmount: number;
  maxPlayerAmount: number;
  categories: number[];
  minPlayerElo: number;
  maxPlayerElo: number;
  status: number;
  inscriptionsEndDate: Date;
  currentRound: number;
  isWomanOnly: boolean;
  players: [];
}
