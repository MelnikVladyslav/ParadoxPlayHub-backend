using BussnesLogic.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussnesLogic.DTO_s
{
    public class GameDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string DeveloperId { get; set; }
        [Url]
        public string ImagePath { get; set; }
        public int GanrId { get; set; }
    }
}
