using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussnesLogic.DTO_s
{
    public class UserDTO
    {
        [Required, MinLength(3)]
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
