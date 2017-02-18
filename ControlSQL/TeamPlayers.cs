using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSQL
{
    class TeamPlayers
    {
        public int Match { get; set; }
        public string Stadium { get; set; }
        
        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public int ScoreA { get; set; }

        public int ScoreB { get; set; }

        public List<Player> PlayersA { get; set; }

        public List<Player> PlayersB { get; set; }
    }
}
