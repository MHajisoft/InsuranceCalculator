using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance.Common.Entity;

public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [ConcurrencyCheck]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Version { get; set; }

    public AppUser CreateUser { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public bool IsTransient() => Id == 0;
}