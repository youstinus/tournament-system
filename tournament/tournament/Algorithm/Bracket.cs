using System;
using System.Collections.Generic;
using tournament.Infrastructure.DataBase.Models;

namespace tournament.Algorithm
{
    public class Bracket
    {
        private readonly List<Match> _all;
        private readonly Tournament _tour;

        public Bracket(Tournament tour, Team[] teams)
        {
            _tour = tour;
            _all = new List<Match>();
            Random random = new Random();
            List<Match> main = new List<Match>();
            List<Match> side = new List<Match>();
            int nextSequence = PutEmptyMatches(main, teams.Length - 1, 1);
            PutEmptyMatches(side, teams.Length - 2, nextSequence);
            Play(main, teams.Length);
            PutLoosers(main, side);
            PlayLoosers(side, teams.Length - 1);
            Final(main, side);
        }

        public List<Match> GetTour()
        {
            return _all;
        }
        private int PutEmptyMatches(List<Match> bracket, int count, int startSeq)
        {
            for (int i = 0; i < count; i++)
            {
                bracket.Add(new Match() { SequenceNr = i + startSeq, TournamentId = _tour.Id});
            }
            startSeq += bracket.Count;
            return startSeq;
        }
        private void Play(List<Match> main, int count)
        {
            if (count % 2 != 0)
            {
                main[0].WinnerGoesToId = main[count / 2].SequenceNr;
            }
            if(count > 2)
            for (int i = count % 2; i < main.Count - 1; i += 2)
            {
                main[i].WinnerGoesToId = main[(count + i) / 2].SequenceNr;
                main[i + 1].WinnerGoesToId = main[(count + i) / 2].SequenceNr;
            }
        }

        private void PutLoosers(List<Match> main, List<Match> side)
        {
            for (int i = 0; i < main.Count-1; i += 2)
            {
                main[i].LoserGoesToId = side[i / 2 + i % 2].SequenceNr;
                main[i + 1].LoserGoesToId = side[i / 2 + i % 2].SequenceNr;
            }
            if(main.Count % 2 != 0)
            {
                main[main.Count - 1].LoserGoesToId = side[(main.Count - 1) / 2].SequenceNr;
            }
        }

        private void PlayLoosers(List<Match> side, int count)
        {
            if (count > 1)
            {
                if (count % 2 != 0)
                {
                    side[0].WinnerGoesToId = side[count / 2].SequenceNr;
                }

                for (int i = count % 2; i < side.Count - 1; i += 2)
                {
                    side[i].WinnerGoesToId = side[(count + i) / 2].SequenceNr;
                    side[i + 1].WinnerGoesToId = side[(count + i) / 2].SequenceNr;
                }
            }
        }

        private void Final(List<Match> main, List<Match> side)
        {
            Match match = new Match();         
            match.TournamentId = _tour.Id;
            match.SequenceNr = main.Count + side.Count + 1;
            main[main.Count - 1].WinnerGoesToId = match.SequenceNr;
            side[side.Count - 1].WinnerGoesToId = match.SequenceNr;
            _all.AddRange(main);
            _all.AddRange(side);
            _all.Add(match);
        }
    }
}
