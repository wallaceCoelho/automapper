using AutoMapper;
using AutoMapper.Interfaces;
using BenchmarkDotNet.Attributes;

namespace Benchmarker;

[MemoryDiagnoser]
public class MapperBenchmarks
{
    Person person;
    Person simplePerson;
    List<Person> listPerson;
    List<Person> listSimplePerson;
    private readonly IMapper _mapper = new Mapper();

    public MapperBenchmarks()
    {
        simplePerson = new Person
        {
            Name = "Wallace",
            Age = 30,
        };
        person = new Person
        {
            Name = "Wallace",
            Age = 30,
            HomeAddress = new Address
            {
                Street = "Rua Exemplo",
                City = "São Paulo",
                Country = "Brasil"
            },
            PreviousAddresses =
            [
                new Address { Street = "Antiga 1", City = "Rio", Country = "Brasil" },
                new Address { Street = "Antiga 2", City = "BH", Country = "Brasil" }
            ]
        };
        
        listPerson = 
        [
            new Person
            {
                Name = "Wallace",
                Age = 30,
                HomeAddress = new Address
                {
                    Street = "Rua Exemplo",
                    City = "São Paulo",
                    Country = "Brasil"
                },
                PreviousAddresses =
                [
                    new Address { Street = "Antiga 1", City = "Rio", Country = "Brasil" },
                    new Address { Street = "Antiga 2", City = "BH", Country = "Brasil" }
                ]
            },
            new Person
            {
                Name = "Wallace2",
                Age = 27,
                HomeAddress = new Address
                {
                    Street = "Rua Exemplo",
                    City = "São Paulo",
                    Country = "Brasil"
                },
                PreviousAddresses =
                [
                    new Address { Street = "Antiga 1", City = "Rio", Country = "Brasil" },
                    new Address { Street = "Antiga 2", City = "BH", Country = "Brasil" }
                ]
            }
        ];
        
        listSimplePerson = 
        [
            new Person
            {
                Name = "Wallace",
                Age = 30,
            },
            new Person
            {
                Name = "Wallace2",
                Age = 27,
            }
        ];
    }
    

    [Benchmark]
    public PersonDto KbrMapperSimple() => MapperKbrtec.Map<Person, PersonDto>(simplePerson);

    [Benchmark]
    public PersonDto CachedMapperSimple() => _mapper.Map<Person, PersonDto>(simplePerson);
    
    [Benchmark]
    public PersonDto KbrMapperComplex() => MapperKbrtec.Map<Person, PersonDto>(person);

    [Benchmark]
    public PersonDto CachedMapperComplex() => _mapper.Map<Person, PersonDto>(person);
    
    [Benchmark]
    public List<PersonDto> KbrMapperListComplex() => MapperKbrtec.Map<Person, PersonDto>(listPerson);

    [Benchmark]
    public List<PersonDto> CachedMapperListComplex() => _mapper.Map<Person, PersonDto>(listPerson);
    
    [Benchmark]
    public List<PersonDto> KbrMapperListSimple() => MapperKbrtec.Map<Person, PersonDto>(listSimplePerson);

    [Benchmark]
    public List<PersonDto> CachedMapperListSimple() => _mapper.Map<Person, PersonDto>(listSimplePerson);
}