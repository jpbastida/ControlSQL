using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace ControlSQL
{
    class ChampRepository
    {
        private readonly string _connectionString
                 = ConfigurationManager.ConnectionStrings["Control"].ConnectionString;

        public IEnumerable<TeamMatches> GetAllMatchesData()
        {
            List<TeamMatches> teamMatches = new List<TeamMatches>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"	SELECT 
	m.Id matchId, m.Stadium Stadium, tA.Name TeamA, tb.Name TeamB, m.TeamAScore ScoreA, m.TeamBScore ScoreB, pA.Name PlayerA, pA.GoalTime GoalA, pB.Name PlayerB, pb.GoalTime GoalB FROM Matches m
	JOIN Teams tA ON tA.Id = m.TeamA
	JOIN Teams tB ON tB.Id = m.TeamB
	JOIN Players pA ON pA.TeamId = tA.Id
	JOIN Players pB ON pB.TeamId = tB.Id
	WHERE tA.Name IS NOT NULL AND tB.Name IS NOT NULL AND pA.Name IS NOT NULL AND pb.Name IS NOT NULL
	ORDER BY m.Id ASC;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teamMatches.Add(new TeamMatches()
                        {
                            Match = (int)reader["matchId"],
                            Stadium = (string)reader["Stadium"],
                            TeamA = (string)reader["TeamA"],
                            TeamB = (string)reader["TeamB"],
                            ScoreA = (int)reader["ScoreA"],
                            ScoreB = (int)reader["ScoreB"],
                            PlayersA = (string)reader["PlayerA"],
                            PlayersB = (string)reader["PlayerB"]
                        });
                    }
                }
                connection.Close();
            }
            return teamMatches;
        }

        public IEnumerable<TeamMatches> GetMatchesAndGoals()
        {
            List<TeamMatches> teamMatches = new List<TeamMatches>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @"	SELECT 
	m.Id matchId, m.Stadium Stadium, tA.Name TeamA, tb.Name TeamB, m.TeamAScore ScoreA, m.TeamBScore ScoreB, pA.Name PlayerA, pA.GoalTime GoalA, pB.Name PlayerB, pb.GoalTime GoalB FROM Matches m
	JOIN Teams tA ON tA.Id = m.TeamA
	JOIN Teams tB ON tB.Id = m.TeamB
	JOIN Players pA ON pA.TeamId = tA.Id
	JOIN Players pB ON pB.TeamId = tB.Id
	WHERE tA.Name IS NOT NULL AND tB.Name IS NOT NULL AND pA.Name IS NOT NULL AND pb.Name IS NOT NULL
	ORDER BY m.Id ASC;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teamMatches.Add(new TeamMatches()
                        {
                            Match = (int)reader["matchId"],
                            Stadium = (string)reader["Stadium"],
                            TeamA = (string)reader["TeamA"],
                            TeamB = (string)reader["TeamB"],
                            ScoreA = (int)reader["ScoreA"],
                            ScoreB = (int)reader["ScoreB"],
                            PlayersA = (string)reader["PlayerA"],
                            GoalA = (string)reader["GoalA"],
                            PlayersB = (string)reader["PlayerB"],
                            GoalB = (string)reader["GoalB"]
                        });
                    }
                }
                connection.Close();
            }
            return teamMatches;
        }

        public void SetGoalsTime()
        {
            Random minute = new Random();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                for (int i = 1; i < 151; i++)
                {
                    string goalTime = string.Format($"{minute.Next(1, 90)}''");
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE Players SET GoalTime = '{goalTime}' WHERE Id= {i.ToString()};";
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }
    }
}
