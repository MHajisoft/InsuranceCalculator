namespace Insurance.Common.Entity;

public class Request : BaseEntity
{
    public Person Person { get; set; }
    public long PersonId { get; set; }

    public InsuranceType Type { get; set; }
    public long TypeId { get; set; }
    
    public long Investment { get; set; }
    
    public long Payment { get; set; }
}