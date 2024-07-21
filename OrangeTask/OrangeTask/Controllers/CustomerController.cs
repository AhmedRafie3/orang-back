using Application;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Services.CustomerService;

namespace OrangeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        [HttpGet]
        public async Task<PaginatedResult<Customer>> GetAll(int pageNumber, int pageSize, bool expirationFlag)
        {
            return await customerService.getExpiredCustomers(pageNumber,pageSize,expirationFlag);
        }

        [HttpGet("GetExpiredCustomersInOneMonth")]
        public async Task<PaginatedResult<Customer>> GetExpiredCustomersInOneMonth(int pageNumber, int pageSize, bool expirationFlag)
        {
            return await customerService.getCustomersInOneMonth(pageNumber, pageSize, expirationFlag);
        }
        [HttpGet("getCustomersByServiceType")]
        public async Task<IEnumerable<ServiceTypeGroup>> getCustomersByServiceType()
        {
            return await customerService.getCustomersByServiceType();
        }
        [HttpGet("getCustomersByYear")]
        public async Task<List<CustomerCountByMonthDto>> getCustomersByYear(int year)
        {
            return await customerService.getCustomersByYear(year);
        }
    }
}
