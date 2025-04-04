using Tests.Entites;

namespace Tests.Mocks;

public static class MockData
{
    public static Person SimplePerson => new()
    {
        Name = "Alice",
        Age = 25,
        HomeAddress = new Address
        {
            Street = "Rua Simples",
            City = "Curitiba",
            Country = "Brasil"
        }
    };

    public static Person PersonWithEmptyAddress => new()
    {
        Name = "Bob",
        Age = 40,
        HomeAddress = new Address
        {
            Street = "",
            City = "",
            Country = "Brasil"
        },
        PreviousAddresses = new List<Address>()
    };

    public static Person PersonWithNullAddress => new()
    {
        Name = "Carla",
        Age = 32,
        HomeAddress = null,
        PreviousAddresses = null
    };

    public static Person ComplexPerson => new()
    {
        Name = "Daniel",
        Age = 38,
        HomeAddress = new Address
        {
            Street = "Av. Central",
            City = "São Paulo",
            Country = "Brasil"
        },
        PreviousAddresses = new List<Address>
        {
            new Address { Street = "Rua A", City = "Campinas", Country = "Brasil" },
            new Address { Street = "Rua B", City = "Santos", Country = "Brasil" },
            new Address { Street = "Rua C", City = "Ribeirão Preto", Country = "Brasil" }
        }
    };

    public static List<Person> ListOfPeople => new()
    {
        SimplePerson,
        PersonWithEmptyAddress,
        PersonWithNullAddress,
        ComplexPerson,
        new Person
        {
            Name = "Eduardo",
            Age = 18,
            HomeAddress = new Address
            {
                Street = "Rua Jovem",
                City = "Florianópolis",
                Country = "Brasil"
            },
            PreviousAddresses = new List<Address>
            {
                new Address { Street = "Rua Velha", City = "Porto Alegre", Country = "Brasil" }
            }
        }
    };
}
