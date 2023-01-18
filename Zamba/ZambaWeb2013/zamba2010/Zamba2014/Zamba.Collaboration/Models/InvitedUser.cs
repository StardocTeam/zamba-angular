using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zamba.Collaboration.Models
{
    public class InvitedUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Cliente { get; set; }
        [Required]
        public string Sector { get; set; }
        [Required]
        public string Puesto { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        public int Interno { get; set; }
        [Required]
        public int Celular { get; set; }

    }
}
