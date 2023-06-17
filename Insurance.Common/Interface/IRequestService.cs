using Insurance.Common.Dto;

namespace Insurance.Common.Interface;

public interface IRequestService : IServiceBase
{
    public Task<RequestDto?> Load(long id);
    
    public Task<RequestDto> Update(RequestApiDto dto);
    
    public Task<PaymentInfoDto> UpdateCollection(CollectionApiDto dto);
    
    public Task<PaymentInfoDto> GetPaymentInfo(long personId);

    public Task Delete(long id);
}