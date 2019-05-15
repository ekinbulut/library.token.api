using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Library.Common.Data;

namespace Library.Authentication.Service.Data.Entities
{
    [Table("PERMISSIONS")]
    [ExcludeFromCodeCoverage]
    public class EPermission : BaseEntity
    {
        [Column("TYPE")] [MaxLength(150)] public virtual string Type { get; set; }
    }
}