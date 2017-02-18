using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            ChampRepository repository = new ChampRepository();
            repository.SetGoalsTime();
            MatchesInfo();
            MatchesAndGoals();
        }

        private static void MatchesInfo()
        {
            ChampRepository repository = new ChampRepository();
            var teamMatchesData = repository.GetAllMatchesData();

            IEnumerable<TeamPlayers> result = teamMatchesData.GroupBy(f => f.Match)
                .Select(f => new TeamPlayers()
                {
                    Match = f.First().Match,
                    Stadium = f.First().Stadium,
                    TeamA = f.First().TeamA,
                    ScoreA = f.First().ScoreA,
                    PlayersA = f.Select(d => new Player()
                    {
                        NamePlayer = d.PlayersA
                    }).ToList(),

                    TeamB = f.First().TeamB,
                    ScoreB = f.First().ScoreB,
                    PlayersB = f.Select(d => new Player()
                    {
                        NamePlayer = d.PlayersB
                    }).ToList()
                });
            ShowMatchesInfo(result);
        }

        private static void MatchesAndGoals()
        {
            ChampRepository repository = new ChampRepository();
            var teamMatchesData = repository.GetMatchesAndGoals();

            IEnumerable<TeamPlayers> result = teamMatchesData.GroupBy(f => f.Match)
                .Select(f => new TeamPlayers()
                {
                    Match = f.First().Match,
                    Stadium = f.First().Stadium,
                    TeamA = f.First().TeamA,
                    ScoreA = f.First().ScoreA,
                    PlayersA = f.Select(d => new Player()
                    {
                        NamePlayer = d.PlayersA,
                        GoalTime = d.GoalA
                    }).ToList(),

                    TeamB = f.First().TeamB,
                    ScoreB = f.First().ScoreB,
                    PlayersB = f.Select(d => new Player()
                    {
                        NamePlayer = d.PlayersB,
                        GoalTime = d.GoalB
                    }).ToList()
                });
            ShowMatchesAndGoals(result);
        }

        private static void ShowMatchesInfo(IEnumerable<TeamPlayers> result)
        {
            foreach (var matchData in result)
            {
                Console.WriteLine($"Match No. {matchData.Match}, Stadium: {matchData.Stadium}");
                Console.WriteLine($"TEAM A: {matchData.TeamA} - TEAM B: {matchData.TeamB}, SCORE: {matchData.ScoreA.ToString()}:{matchData.ScoreB.ToString()}");
                Console.WriteLine("--------------------");
                Console.WriteLine();
                Console.WriteLine($"{matchData.TeamA}, players in this match:");
                Console.WriteLine();
                foreach (var p in matchData.PlayersA.Select(x => x.NamePlayer).Distinct())
                {
                    Console.WriteLine($"\t{p}");
                }
                Console.WriteLine();
                Console.WriteLine($"{matchData.TeamB}, players in this match:");
                Console.WriteLine();
                foreach (var p in matchData.PlayersB.Select(x => x.NamePlayer).Distinct())
                {
                    Console.WriteLine($"\t{p}");
                }
                Console.WriteLine();
            }
        }

        private static void ShowMatchesAndGoals(IEnumerable<TeamPlayers> result)
        {
            foreach (var matchData in result)
            {
                Console.WriteLine($"Match No. {matchData.Match}, Stadium: {matchData.Stadium}");
                Console.WriteLine($"TEAM A: {matchData.TeamA} - TEAM B: {matchData.TeamB}, SCORE: {matchData.ScoreA.ToString()}:{matchData.ScoreB.ToString()}");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();

                foreach (var goalTime in matchData.PlayersA.Select(x => new { x.NamePlayer, x.GoalTime }).Distinct().OrderBy(f => f.GoalTime))
                {
                    Console.WriteLine($"{goalTime.GoalTime} {goalTime.NamePlayer}");
                }
                Console.WriteLine();
                foreach (var goalTime in matchData.PlayersB.Select(x => new { x.NamePlayer, x.GoalTime }).Distinct().OrderBy(f => f.GoalTime))
                {
                    Console.WriteLine($"\t\t{goalTime.GoalTime} {goalTime.NamePlayer}");
                }
                Console.WriteLine();
                Console.WriteLine("-----------------------------------");
                Console.WriteLine();
                Console.WriteLine($"{matchData.TeamA}, players in this match:");
                Console.WriteLine();
                foreach (var p in matchData.PlayersA.Select(x => x.NamePlayer).Distinct())
                {
                    Console.WriteLine($"\t{p}");
                }
                Console.WriteLine();
                Console.WriteLine($"{matchData.TeamB}, players in this match:");
                Console.WriteLine();
                foreach (var p in matchData.PlayersB.Select(x => x.NamePlayer).Distinct())
                {
                    Console.WriteLine($"\t{p}");
                }
                Console.WriteLine();
            }
        }
    }
}
