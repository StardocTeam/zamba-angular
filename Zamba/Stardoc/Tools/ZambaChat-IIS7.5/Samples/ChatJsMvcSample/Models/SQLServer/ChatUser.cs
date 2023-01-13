using ChatJsMvcSample.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ChatJsMvcSample.Models
{
    // [Table("CHATUSERS", Schema = "ZAMBA")]//ORACLE
    public class ChatUser
    {
        public ChatUser()
        {
            this.Status = StatusType.Offline;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public decimal Id { get; set; }

        [Column("NAME")]
        [Display(Name = "Nombre y apellido")]
        [Required(ErrorMessage = "Por favor ingrese su nombre y apellido")]
        public string Name { get; set; }

        [Column("AVATAR")]
        [Display(Name = "Avatar")]
        public string Avatar { get; set; }

        [Column("LASTACTIVEON")]//, TypeName = "datetime2"
        //[Column(TypeName = "datetime2")]
        public DateTime LastActiveOn { get; set; }

        [Column("ROOMID")]
        public string RoomId { get; set; }

        [NotMapped]
        public StatusType Status { get; set; }//StatusType
        //EF 4.1 no soporta guardar enum porque son Int, se debe tratar como decimal
        [Column("STATUS")]
        public decimal StatusDecimal
        {
            get { return Convert.ToDecimal(Status); }
            private set { Status = EnumExtensions.ParseEnum<StatusType>(value); }
        }

        [NotMapped]
        public RoleType Role { get; set; }

        [Column("ROLE")]
        public decimal RoleDecimal
        {
            get { return Convert.ToDecimal(Role); }
            private set { Role = EnumExtensions.ParseEnum<RoleType>(value); }
        }

        // SQL
        //  [Column("ROLE")]
        [NotMapped]
        public int? DocId { get; set; }

        [NotMapped]
        public DateTime LastMessage { get; set; }//No mapeado, ultimo mensaje de usuario

        public enum StatusType
        {
            Offline = 0,
            Online = 1,
            Busy = 2,
            DontDisturb = 3
        }

        public enum RoleType
        {
            Active = 0,
            Listener = 1,
            Blocked = 2
        }

        #region Zamba Collaboration
        [Column("EMAIL")]
        [Display(Name = "Dirección de mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]        
        public string Email { get; set; }

        [Column("COMPANY")]
        [Display(Name = "Empresa")]        
        public string Company { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Reingrese contraseña")]
        [Compare("Password", ErrorMessage = "*Las contraseñas no coinciden")]
        public string RetryPassword { get; set; }


        [StringLength(int.MaxValue, ErrorMessage = "*Debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "*Contraseña obligatoria")]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
        #endregion

        #region Opcionales
        [Display(Name = "Puesto (Opcional)")]
        public string Position { get; set; } = "";
        [Display(Name = "Teléfono (Opcional)")]
        public string Phone { get; set; } = "";
        [Display(Name = "Interno (Opcional)")]
        public string InternalPhone { get; set; } = "";
        [Display(Name = "Celular (Opcional)")]
        public string CellPhone { get; set; } = "";
        #endregion
    }
    public class EnumExtensions
    {
        public static T ParseEnum<T>(decimal value)
        {
            return (T)(Enum.Parse(typeof(T), value.ToString(), true));
        }
    }
}