using AutoMapper;
using Insurance.Common.Dto;
using Insurance.Common.Entity;
using Insurance.Common.Interface;
using Insurance.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Service.Services;

public class RequestService : ServiceBase<Request>, IRequestService
{
    public RequestService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<RequestDto?> Load(long id)
    {
        var result = await DataSet.Where(x => x.Id == id).Include(nameof(Request.Person)).Include(nameof(Request.Type)).SingleOrDefaultAsync();

        return Mapper.Map<RequestDto>(result);
    }

    public async Task<RequestDto> Update(RequestApiDto dto)
    {
        Request result;

        if (dto.IsTransient())
            result = new Request();
        else
            result = await DataSet.SingleAsync(x => x.Id == dto.Id);

        result = Mapper.Map(dto, result);

        result.Person = await DbContext.Persons.SingleAsync(x => x.Id == dto.PersonId);
        result.Type = await DbContext.InsuranceTypes.SingleAsync(x => x.Id == dto.TypeId);
        result.Payment = (long)(dto.Investment * result.Type.PaymentFactor);

        //Validate
        if (dto.Investment < result.Type.MinInvest || dto.Investment > result.Type.MaxInvest)
            throw new Exception("Investment range is not valid");

        if (dto.IsTransient())
            DataSet.Add(result);

        await DbContext.SaveChangesAsync();

        return Mapper.Map<RequestDto>(result);
    }

    public async Task<PaymentInfoDto> UpdateCollection(CollectionApiDto dto)
    {
        //Delete Old Data
        var requestList = await DataSet.Where(x => x.PersonId == dto.PersonId).ToListAsync();
        if (requestList.Any())
            DataSet.RemoveRange(requestList);

        var person = await DbContext.Persons.SingleAsync(x => x.Id == dto.PersonId);
        var list = new List<Request>();

        foreach (var item in dto.TypeList)
        {
            var instance = new Request
            {
                Title = dto.Title,
                Person = person,
                Type = await DbContext.InsuranceTypes.SingleAsync(x => x.Id == item.Key),
                Investment = item.Value,
            };
            instance.Payment = (long)(instance.Investment * instance.Type.PaymentFactor);

            //Validate
            if (instance.Investment < instance.Type.MinInvest || instance.Investment > instance.Type.MaxInvest)
                throw new Exception("Investment range is not valid");

            list.Add(instance);
        }

        DataSet.AddRange(list);

        await DbContext.SaveChangesAsync();

        return await GetPaymentInfo(dto.PersonId);
    }

    public async Task<PaymentInfoDto> GetPaymentInfo(long personId)
    {
        var requestList = await DataSet.Where(x => x.PersonId == personId).Include(nameof(Request.Person)).Include(nameof(Request.Type)).ToListAsync();
        var result = new PaymentInfoDto();

        if (requestList.Any())
        {
            result.TypeTitleList = requestList.Select(x => x.Type.Title).ToList();
            result.Person = requestList.First().Person.FullName;
            result.Title = requestList.First().Title;
            result.TotalPayment = (long)requestList.Sum(x => x.Investment * x.Type.PaymentFactor);
        }

        return result;
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