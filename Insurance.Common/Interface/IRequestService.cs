using Insurance.Common.Dto;

namespace Insurance.Common.Interface;

public interface IRequestService : IServiceBase
{
    public Task<RequestDto?> Load(long id);
    
    public Task<RequestDto> Update(RequestDto dto);
    
    public Task Delete(long id);
}