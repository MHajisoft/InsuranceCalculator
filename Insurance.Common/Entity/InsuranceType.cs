namespace Insurance.Common.Entity;

public class InsuranceType : BaseEntity
{
    public string Title { get; set; }

    public int MinInvest { get; set; }

    public long MaxInvest { get; set; }
    
    public float PaymentFactor { get; set; }
}