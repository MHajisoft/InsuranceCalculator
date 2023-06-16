namespace Insurance.Common.Dto;

public class PersonDto : BaseDto
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? NationalCode { get; set; }
}