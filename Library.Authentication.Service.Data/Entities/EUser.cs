using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Library.Common.Data;

namespace Library.Authentication.Service.Data.Entities
{
    [Table("USERS")]
    [ExcludeFromCodeCoverage]
    public class EUser : BaseEntity
    {
        [Column("USERNAME", Order = 2)]
        [MaxLength(150)]
        public virtual string Username { get; set; }

        [Column("PASSWORD", Order = 3)]
        [MaxLength(150)]
        public virtual string Password { get; set; }

        [Column("NAME", Order = 1)]
        [MaxLength(150)]
        public virtual string Name { get; set; }

        [Column("EMAIL", Order = 4)]
        [MaxLength(50)]
        public virtual string Email { get; set; }

        [Column("ROLE_ID")]
        [ForeignKey("ROLE_ID")]
        [MaxLength(1)]
        public virtual int RoleId { get; set; }

        public virtual ERole Role { get; set; }
    }
}