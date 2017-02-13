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

            var teamMatchesData = repository.GetAllMatchesData();

            foreach (var tmData in teamMatchesData)
            {
                Console.WriteLine($"{tmData.TeamA} - {tmData.TeamB} {tmData.ScoreA.ToString()}:{tmData.ScoreB.ToString()}");

                Console.WriteLine();
            }

        }
    }
}
