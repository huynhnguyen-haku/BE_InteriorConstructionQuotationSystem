using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SWP391API.Models;

namespace SWP391API.Repositories
{
    public class Repository<T> : RepositoryBase<T> where T : class
    {
        private readonly InteriorConstructionQuotationSystemContext _dbContext;
        public Repository(InteriorConstructionQuotationSystemContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
