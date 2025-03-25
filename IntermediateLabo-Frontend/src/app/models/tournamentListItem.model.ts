import {TournamentCatEnum} from '../enums/tournament-cat-enum';

export interface TournamentListItem {
  name: string;
  location?: string;
  playerAmount: number;
  minPlayerAmount: number;
  maxPlayerAmount: number;
  categories: number[];
  minPlayerElo: number;
  maxPlayerElo: number;
  status: number;
  inscriptionsEndDate: Date;
  currentRound: number;
}
