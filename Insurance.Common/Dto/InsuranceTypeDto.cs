namespace Insurance.Common.Dto;

public class InsuranceTypeDto : BaseDto
{
    public string Title { get; set; }

    public int MinInvest { get; set; }

    public long MaxInvest { get; set; }
    
    public double PaymentFactor { get; set; }
}