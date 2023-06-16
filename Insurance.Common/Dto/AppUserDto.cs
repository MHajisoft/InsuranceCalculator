namespace Insurance.Common.Dto;

public class AppUserDto : BaseDto
{
    public string Title { get; set; }

    public string UserName { get; set; }
    
    public string Token { get; set; }
}