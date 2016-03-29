using DDDSkeletonNET.Infrastructure.Common.Domain;

namespace DDDSkeletonNET.Portal.Domain.Customer
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
    }
}