using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlow.Domain.Entities;

public abstract class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedBy { get; set; }
    public bool IsDeleted => DeletedAt is not null ? true : false;
}
