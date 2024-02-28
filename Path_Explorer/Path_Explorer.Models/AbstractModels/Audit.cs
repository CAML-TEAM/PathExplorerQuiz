using System.ComponentModel.DataAnnotations;

namespace Path_Explorer.Models.AbstractModel;

public abstract class BaseAudit : ISoftDelete
{
    [StringLength(75)]
    public string CreatedBy { get; set; } = "SYSTEM";
    public DateTime DateCreated { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DateUpdated { get; set; }
    public bool SoftDeleted { get; set; }
    public string? Status { get;set; }
}
