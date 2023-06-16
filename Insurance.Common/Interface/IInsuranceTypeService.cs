using Insurance.Common.Dto;

namespace Insurance.Common.Interface;

public interface IInsuranceTypeService : IServiceBase
{
    public Task<InsuranceTypeDto?> Load(long id);
    
    public Task<InsuranceTypeDto> Update(InsuranceTypeDto dto);
    
    public Task Delete(long id);
}