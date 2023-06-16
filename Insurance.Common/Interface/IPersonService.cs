using Insurance.Common.Dto;

namespace Insurance.Common.Interface;

public interface IPersonService : IServiceBase
{
    public Task<PersonDto?> Load(long id);
    
    public Task<PersonDto> Update(PersonDto dto);
    
    public Task Delete(long id);
}