using AutoMapper;
using Insurance.Common.Dto;
using Insurance.Common.Entity;
using Insurance.Common.Interface;
using Insurance.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Service.Services;

public class InsuranceTypeService : ServiceBase<InsuranceType>, IInsuranceTypeService
{
    public InsuranceTypeService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<InsuranceTypeDto?> Load(long id)
    {
        var result = await DataSet.SingleOrDefaultAsync(x => x.Id == id);

        return Mapper.Map<InsuranceTypeDto>(result);
    }

    public async Task<InsuranceTypeDto> Update(InsuranceTypeDto dto)
    {
        InsuranceType result;
        if (dto.IsTransient())
        {
            result = Mapper.Map<InsuranceType>(dto);

            DataSet.Add(result);
        }
        else
        {
            result = await DataSet.SingleAsync(x => x.Id == dto.Id);
            result = Mapper.Map(dto, result);
        }

        await DbContext.SaveChangesAsync();

        return Mapper.Map<InsuranceTypeDto>(result);
    }

    public async Task Delete(long id)
    {
        var result = await DataSet.SingleOrDefaultAsync(x => x.Id == id);

        if (result == null)
            throw new Exception("Id is not valid!");

        DataSet.Remove(result);

        await DbContext.SaveChangesAsync();
    }
}