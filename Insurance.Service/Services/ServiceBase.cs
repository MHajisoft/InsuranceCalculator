using AutoMapper;
using Insurance.Common.Entity;
using Insurance.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Service.Services;

public abstract class ServiceBase<T> where T : BaseEntity
{
    protected AppDbContext DbContext { get; set; }

    protected IMapper Mapper { get; set; }

    protected DbSet<T> DataSet { get; set; }

    protected ServiceBase(AppDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
        DataSet = dbContext.Set<T>();
    }
}