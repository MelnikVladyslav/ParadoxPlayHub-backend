using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussnesLogic.Entity
{
    public enum TypeG : int
    {
        Action = 1,
        RPG,
        Strategy,
        Adventure,
        Racing,
        Sports,
        Horror,
        Simulation,
        Indie,
        Multiplayer,
        Survival,
        Arcade,
        VR,
        InteractiveFiction
    }

    public class Ganr
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PidGanreGame> Games { get; set; } = new HashSet<PidGanreGame>();

    }
}
