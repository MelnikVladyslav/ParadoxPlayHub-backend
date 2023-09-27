using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussnesLogic.Entity
{
    public class PidGanreGame
    {
        public int GanrId { get; set; }
        public Ganr? Ganr { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
    }
}
