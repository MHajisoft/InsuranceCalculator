namespace Insurance.Common.Entity;

public class Person : BaseEntity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string? NationalCode { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}