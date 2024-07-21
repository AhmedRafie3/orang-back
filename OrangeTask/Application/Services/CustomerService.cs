using Application.Interfaces;
using Application.Repository.IBase;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerService(IUnitOfWork _unitOfWork) : ICustomerService
    {
        public async Task<IEnumerable<ServiceTypeGroup>> getCustomersByServiceType()
        {
            var query = _unitOfWork.Repository<Customer>().FindAll();

            var groupedData = await query
                .GroupBy(c => c.Service)
                .Select(g => new ServiceTypeGroup
                {
                    ServiceType = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return groupedData;
        }

        public async Task<List<CustomerCountByMonthDto>> getCustomersByYear(int year)
        {
            var query = _unitOfWork.Repository<Customer>().FindAll()
       .Where(c => c.ContractDate.Year == year)  // Filter by the specified year
       .GroupBy(c => new { c.ContractDate.Year, c.ContractDate.Month })
       .Select(g => new CustomerCountByMonthDto
       {
           Year = g.Key.Year,
           Month = g.Key.Month,
           Count = g.Count()
       });

            var data = await query.ToListAsync();
            return data;


        }

        public async Task<PaginatedResult<Customer>> getCustomersInOneMonth(int pageNumber, int pageSize, bool expirationWitninMonth)
        {
            var query = _unitOfWork.Repository<Customer>().FindAll();

            if (expirationWitninMonth)
            {
                query = query.Where(c => c.ContractExpiryDate > c.ContractDate.AddMonths(11));
            }

            var totalCount = await query.CountAsync();

            var customers = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Customer>
            {
                Items = customers,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<Customer>> getExpiredCustomers(int pageNumber, int pageSize, bool expirationFlag)
        {
            var query = _unitOfWork.Repository<Customer>().FindAll();

            if (expirationFlag)
            {
                query = query.Where(c => c.ContractExpiryDate > c.ContractDate.AddYears(1));
            }

            var totalCount = await query.CountAsync();

            var customers = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Customer>
            {
                Items = customers,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public class ServiceTypeGroup
        {
            public string ServiceType { get; set; }
            public int Count { get; set; }
        }
        public class CustomerCountByMonthDto
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int Count { get; set; }
        }


    }

}
