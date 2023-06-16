namespace Insurance.Common.Entity;

public abstract class BaseEntity
{
    public long Id { get; set; }

    public byte[] Version { get; set; }

    public AppUser CreateUser { get; set; }
    public long CreateUserId { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsTransient() => Id == 0;
}