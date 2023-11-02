using BussnesLogic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussnesLogic.DTO_s
{
    public class LibraryDTO
    {
        public string Email { get; set; }
        public ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
