using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using vT.eCoffeeShop.Domain.Models;
using vT.eCoffeeShop.Infrastructure.Contexts.OrderContexts;
using vT.eCoffeeShop.Infrastructure.Models;

namespace vT.eCoffeeShop.OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoffeeController : ControllerBase
{
    private readonly SqlDbContextOrder _dbContext;

    // Sample in-memory storage for demonstration purposes
    public CoffeeController(SqlDbContextOrder dbContext)
    {
        _dbContext = dbContext;
    }

    // GET api/coffee
    [HttpGet("fetchallcoffee")]
    [OutputCache(Duration = 86400)]
    public async Task<ActionResult<IEnumerable<string>>> GetAsync()
    {
        try
        {
            Console.WriteLine("fetchallcoffee: Order Service Load Started");
            var cofeeItems = await _dbContext.CoffeeItems.Select(x =>
                new CoffeeItemModel
                {
                    CoffeeItemId = x.CoffeeItemId,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    IsAvailable = x.IsAvailable,
                    Name = x.Name,
                    Price = x.Price,
                    Weight = x.Weight
                }).ToListAsync();
            return Ok(cofeeItems);
        }
        catch (Exception ex)
        {
            // Log the error (optional)
            Console.WriteLine($"fetchallcoffee: Error: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
        finally
        {
            Console.WriteLine("fetchallcoffee: Order Service Load Ended");
        }
    }

    // GET api/coffee/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CoffeeItemModel>> Get(string id)
    {
        CoffeeItemModel? cofeeItems = null;
        try
        {
            Console.WriteLine("fetchallcoffee: Order Service Load Started");
            var result = await _dbContext.CoffeeItems.SingleOrDefaultAsync(x => x.CoffeeItemId == new Guid(id));
            if (result != null)
                cofeeItems = new CoffeeItemModel
                {
                    CoffeeItemId = result.CoffeeItemId,
                    Description = result.Description,
                    ImageUrl = result.ImageUrl,
                    IsAvailable = result.IsAvailable,
                    Name = result.Name,
                    Price = result.Price,
                    Weight = result.Weight
                };

            return Ok(cofeeItems);
        }
        catch (Exception ex)
        {
            // Log the error (optional)
            Console.WriteLine($"fetchallcoffee: Error: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
        finally
        {
            Console.WriteLine("fetchallcoffee: Order Service Load Ended");
        }
    }

    // POST api/coffee
    [HttpPost]
    public ActionResult Post([FromBody] CoffeeItemModel coffee)
    {
        CoffeeItemDto cofeeItem = new()
        {
            Description = coffee.Description,
            ImageUrl = coffee.ImageUrl,
            IsAvailable = coffee.IsAvailable,
            Name = coffee.Name,
            Price = coffee.Price,
            Weight = coffee.Weight
        };
        _dbContext.CoffeeItems.Add(cofeeItem);
        _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = cofeeItem.CoffeeItemId }, coffee);
    }

    // DELETE api/coffee/5
    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        _dbContext.CoffeeItems.Remove(_dbContext.CoffeeItems.Single(x => x.CoffeeItemId == new Guid(id)));

        _dbContext.SaveChangesAsync();
        return NoContent();
    }
}