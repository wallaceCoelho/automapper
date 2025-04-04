using AutoMapper;
using AutoMapper.Interfaces;
using Tests.Entites;

namespace Tests;

public class MapperUpdateTests
{
    private readonly IMapper _mapper = new Mapper();

    [Fact]
    public void Should_Update_Existing_Simple_Object()
    {
        var person = new Person
        {
            Name = "Antigo",
            Age = 50
        };

        var dto = new PersonDto
        {
            Name = "Atualizado",
            Age = 28
        };

        _mapper.Map(dto, person);

        Assert.Equal("Atualizado", person.Name);
        Assert.Equal(28, person.Age);
    }

    [Fact]
    public void Should_Update_Existing_Complex_Object()
    {
        var person = new Person
        {
            Name = "João",
            Age = 40,
            HomeAddress = new Address
            {
                Street = "Rua antiga",
                City = "Cidade antiga",
                Country = "Brasil"
            },
            PreviousAddresses = new List<Address>
            {
                new Address { Street = "Velha 1", City = "Cidade 1", Country = "Brasil" }
            }
        };

        var dto = new PersonDto
        {
            Name = "Carlos",
            Age = 45,
            HomeAddress = new AddressDto
            {
                Street = "Rua Nova",
                City = "Cidade Nova"
            },
            PreviousAddresses = new List<AddressDto>
            {
                new AddressDto { Street = "Nova 1", City = "Cidade 1" },
                new AddressDto { Street = "Nova 2", City = "Cidade 2" }
            }
        };

        _mapper.Map(dto, person);

        Assert.Equal("Carlos", person.Name);
        Assert.Equal(45, person.Age);
        Assert.NotNull(person.HomeAddress);
        Assert.Equal("Rua Nova", person.HomeAddress.Street);
        Assert.Equal("Cidade Nova", person.HomeAddress.City);
        Assert.Equal("Brasil", person.HomeAddress.Country); // Ignorado no DTO

        Assert.Equal(2, person.PreviousAddresses.Count);
        Assert.Equal("Nova 1", person.PreviousAddresses[0].Street);
        Assert.Equal("Nova 2", person.PreviousAddresses[1].Street);
    }

    [Fact]
    public void Should_Update_Existing_List()
    {
        var people = new List<Person>
        {
            new Person { Name = "Antigo1", Age = 10 },
            new Person { Name = "Antigo2", Age = 20 }
        };

        var dtos = new List<PersonDto>
        {
            new PersonDto { Name = "Novo1", Age = 30 },
            new PersonDto { Name = "Novo2", Age = 40 }
        };

        for (int i = 0; i < dtos.Count; i++)
        {
            _mapper.Map(dtos[i], people[i]);
        }

        Assert.Equal("Novo1", people[0].Name);
        Assert.Equal(30, people[0].Age);
        Assert.Equal("Novo2", people[1].Name);
        Assert.Equal(40, people[1].Age);
    }

    [Fact]
    public void Should_Update_Existing_Nested_List()
    {
        var people = new List<List<Person>>
        {
            new List<Person>
            {
                new Person { Name = "Velho1", Age = 60 },
                new Person { Name = "Velho2", Age = 65 }
            },
            new List<Person>
            {
                new Person { Name = "Velho3", Age = 70 }
            }
        };

        var dtos = new List<List<PersonDto>>
        {
            new List<PersonDto>
            {
                new PersonDto { Name = "Novo1", Age = 30 },
                new PersonDto { Name = "Novo2", Age = 35 }
            },
            new List<PersonDto>
            {
                new PersonDto { Name = "Novo3", Age = 40 }
            }
        };

        for (int i = 0; i < dtos.Count; i++)
        {
            for (int j = 0; j < dtos[i].Count; j++)
            {
                _mapper.Map(dtos[i][j], people[i][j]);
            }
        }

        Assert.Equal("Novo1", people[0][0].Name);
        Assert.Equal("Novo2", people[0][1].Name);
        Assert.Equal("Novo3", people[1][0].Name);
    }
}
