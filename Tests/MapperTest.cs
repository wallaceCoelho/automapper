using AutoMapper;
using AutoMapper.Interfaces;
using Tests.Entites;

namespace Tests;

public class MapperTest
{
    private readonly IMapper _mapper = new Mapper();
    
    [Fact]
    public void Should_Map_Simple_Object_Correctly()
    {
        var person = new Person()
        {
            Name = "Alice",
            Age = 28
        };

        var result = _mapper.Map<Person, PersonDto>(person);

        Assert.Equal("Alice", result.Name);
        Assert.Equal(28, result.Age);
        Assert.Null(result.HomeAddress);
        Assert.Empty(result.PreviousAddresses);
    }

    [Fact]
    public void Should_Map_Complex_Object_Correctly()
    {
        var person = new Person
        {
            Name = "Bob",
            Age = 35,
            HomeAddress = new Address { Street = "Main", City = "NY", Country = "USA" },
            PreviousAddresses = new List<Address>
            {
                new Address { Street = "Old 1", City = "Boston", Country = "USA" },
                new Address { Street = "Old 2", City = "Chicago", Country = "USA" }
            }
        };

        var result = _mapper.Map<Person, PersonDto>(person);

        Assert.Equal("Bob", result.Name);
        Assert.Equal(35, result.Age);
        Assert.NotNull(result.HomeAddress);
        Assert.Equal("Main", result.HomeAddress.Street);
        Assert.Equal("NY", result.HomeAddress.City);
        Assert.Null(result.HomeAddress.GetType().GetProperty("Country")); // Ignorado
        Assert.Equal(2, result.PreviousAddresses.Count);
        Assert.DoesNotContain(result.PreviousAddresses, a => a.City == null);
    }

    [Fact]
    public void Should_Map_List_Correctly()
    {
        var list = new List<Person>
        {
            new Person { Name = "Carlos", Age = 40 },
            new Person { Name = "Diana", Age = 25 }
        };

        var result = _mapper.Map<Person, PersonDto>(list);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.Name == "Carlos");
        Assert.Contains(result, p => p.Name == "Diana");
    }

    [Fact]
    public void Should_Map_Nested_List_Of_Lists_Correctly()
    {
        var nested = new List<List<Person>>
        {
            new List<Person>
            {
                new Person { Name = "Elena", Age = 32 },
                new Person { Name = "Fabio", Age = 38 }
            },
            new List<Person>
            {
                new Person { Name = "Giovana", Age = 21 }
            }
        };

        var result = _mapper.Map<Person, PersonDto>(nested);

        Assert.Equal(2, result.Count());
        Assert.All(result, sublist => Assert.All(sublist, p => Assert.NotNull(p.Name)));
    }

    [Fact]
    public void Should_Map_To_Existing_Instance()
    {
        var person = new Person
        {
            Name = "Helen",
            Age = 29,
            HomeAddress = new Address { Street = "Maple", City = "Denver", Country = "USA" }
        };

        var destination = new PersonDto();
        _mapper.Map(person, destination);

        Assert.Equal("Helen", destination.Name);
        Assert.Equal(29, destination.Age);
        Assert.Equal("Maple", destination.HomeAddress.Street);
        Assert.Equal("Denver", destination.HomeAddress.City);
    }
}