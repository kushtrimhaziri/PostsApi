using System.ComponentModel.DataAnnotations.Schema;

namespace PostsApi.Core.Domain;

public class BaseEntity
{
    #region Properties

    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }    
    public string? CreatedBy { get; set; }
    
    public DateTime? UpdatedOn { get; set; }
    [Column]
    public string? UpdatedBy { get; set; }
    
    public bool Deleted { get; set; } = false;
    
    #endregion Properties

    public virtual string GetEntityName()
    {
        return GetType().Name;
    }

    public virtual int GetEntityId()
    {
        return Id;
    }
}
