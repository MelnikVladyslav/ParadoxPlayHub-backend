using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussnesLogic.Entity
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string DeveloperId { get; set; }
        public User? Developer { get; set; }
        [Url]
        public string ImagePath { get; set; }
        public ICollection<PidGanreGame> Ganres { get; set; } = new HashSet<PidGanreGame>();

    }
}
