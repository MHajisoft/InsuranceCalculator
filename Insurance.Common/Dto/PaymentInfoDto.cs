namespace Insurance.Common.Dto;

public class PaymentInfoDto
{
    public List<string> TypeTitleList { get; set; }
    
    public string Title { get; set; }
    
    public string Person { get; set; }
    
    public long TotalPayment { get; set; }
}