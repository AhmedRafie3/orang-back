using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Services.CustomerService;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<PaginatedResult<Customer>> getExpiredCustomers(int pageNumber, int pageSize, bool expirationFlag);
        Task<PaginatedResult<Customer>> getCustomersInOneMonth(int pageNumber, int pageSize, bool expirationWitninMonth);
        Task<IEnumerable<ServiceTypeGroup>> getCustomersByServiceType();
        Task<List<CustomerCountByMonthDto>> getCustomersByYear(int year);
    }
}
