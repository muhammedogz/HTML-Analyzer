using test_dotnet.Models;

namespace test_dotnet.Services;

public static class PizzaService
{
  static List<Pizza> Pizzas { get; }
  static int nextId { get; set; } = 0;

  static PizzaService()
  {
    Pizzas = new List<Pizza>()
    {
      new Pizza {Id = nextId++, Name = "Pepperoni", Price = 10.99},
      new Pizza {Id = nextId++, Name = "Cheese", Price = 8.99},
      new Pizza {Id = nextId++, Name = "Veggie", Price = 11.99},
    };
  }

  public static List<Pizza> GetAll() => Pizzas;

  public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

  public static void Add(Pizza pizza)
  {
    pizza.Id = nextId++;
    Pizzas.Add(pizza);
  }

  public static void Delete(int id)
  {
    Pizza? pizza = Get(id);
    if (pizza is not null)
    {
      Pizzas.Remove(pizza);
    }
  }

  public static void Update(Pizza pizza)
  {
    Pizza? oldPizza = Get(pizza.Id);
    if (oldPizza is not null)
    {
      oldPizza.Name = pizza.Name;
      oldPizza.Description = pizza.Description;
      oldPizza.Price = pizza.Price;
    }
  }

}