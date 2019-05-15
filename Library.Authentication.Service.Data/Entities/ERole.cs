using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Library.Common.Data;

namespace Library.Authentication.Service.Data.Entities
{
    [Table("ROLES")]
    [ExcludeFromCodeCoverage]
    public class ERole : BaseEntity
    {
        public ERole()
        {
            Permissions = new List<EPermission>();
            Users = new List<EUser>();
        }

        [MaxLength(150)]
        [Column("NAME", Order = 1)]
        public virtual string Name { get; set; }

        public virtual ICollection<EPermission> Permissions { get; set; }

        public virtual ICollection<EUser> Users { get; set; }
    }
}