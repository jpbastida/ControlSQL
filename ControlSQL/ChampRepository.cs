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
	                    tA.Name TeamA, tb.Name TeamB, m.TeamAScore ScoreA, m.TeamBScore ScoreB FROM Matches m
	                    JOIN Teams tA ON tA.Id = m.TeamA
	                    JOIN Teams tB ON tB.Id = m.TeamB
                        WHERE tA.Name IS NOT NULL
                        AND tB.Name IS NOT NULL;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TeamMatches dataTeamMatches = new TeamMatches()
                        {
                            TeamA = (string)reader["TeamA"],
                            TeamB = (string)reader["TeamB"],
                            ScoreA = (int)reader["ScoreA"],
                            ScoreB = (int)reader["ScoreB"]
                        };
                        teamMatches.Add(dataTeamMatches);
                    }
                }
                connection.Close();
            }
            return teamMatches;
        }

    }
}
