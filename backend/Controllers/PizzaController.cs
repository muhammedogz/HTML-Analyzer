using Microsoft.AspNetCore.Mvc;

namespace test_dotnet.Controllers;

using test_dotnet.Models;
using test_dotnet.Services;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
  [HttpGet(Name = "GetPizzas")]
  public ActionResult<List<Pizza>> Get()
  {
    return PizzaService.GetAll();
  }

  [HttpGet("{id}", Name = "GetPizza")]
  public ActionResult<Pizza> Get(int id)
  {
    Pizza? pizza = PizzaService.Get(id);
    if (pizza is null)
    {
      return NotFound();
    }
    return pizza;
  }

  [HttpPost(Name = "AddPizza")]
  public ActionResult<Pizza> Add(Pizza pizza)
  {
    PizzaService.Add(pizza);
    return CreatedAtRoute("GetPizza", new { id = pizza.Id }, pizza);
  }

  [HttpPut(Name = "UpdatePizza")]
  public ActionResult Update(Pizza pizza)
  {
    PizzaService.Update(pizza);
    return NoContent();
  }

  [HttpDelete("{id}", Name = "DeletePizza")]
  public ActionResult Delete(int id)
  {
    PizzaService.Delete(id);
    return NoContent();
  }
}