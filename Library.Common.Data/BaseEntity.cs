using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Library.Common.Data
{
    /// <summary>
    ///     Template of an entity
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class BaseEntity
    {
        [Key] [Column("ID", Order = 0)] public int Id { get; set; }

        [Column("CREATED", Order = 6, TypeName = "datetime2")]
        public DateTime Created { get; set; } = DateTime.Now;

        [Column("UPDATED", Order = 7, TypeName = "datetime2")]
        public DateTime Updated { get; set; } = DateTime.Now;

        [Column("CREATED_BY", Order = 8)] public string CreatedBy { get; set; }

        [Column("UPDATED_BY", Order = 9)] public string UpdatedBy { get; set; }
    }
}