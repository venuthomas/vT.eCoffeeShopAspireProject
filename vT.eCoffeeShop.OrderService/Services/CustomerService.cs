using AutoMapper;
using vT.eCoffeeShop.Domain.Models;
using vT.eCoffeeShop.Infrastructure.Contexts.OrderContexts;
using vT.eCoffeeShop.Infrastructure.Models;

namespace vT.eCoffeeShop.OrderService.Services;

public class CustomerService
{
    private readonly SqlDbContextOrder _dbContext;
    private readonly IMapper _mapper;

    public CustomerService(SqlDbContextOrder dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<string?> SaveCustomerAsync(CustumersModel customerDto)
    {
        try
        {
            var customer = _mapper.Map<CustomerDto>(customerDto);
            customer.CustomerId = Guid.NewGuid();
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return customer.CustomerId.ToString();
        }
        catch (Exception)
        {
            return default;
        }
    }
}