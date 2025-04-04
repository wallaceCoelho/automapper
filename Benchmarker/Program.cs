using BenchmarkDotNet.Running;
using Benchmarker;
using AutoMapper;
using AutoMapper.Interfaces;

public class Program
{
    private readonly IMapper _mapper = new Mapper();
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<MapperBenchmarks>();
        // try
        // {
        //     var simplePerson = new Person
        //     {
        //         Name = "Wallace",
        //         Age = 30,
        //     };
        //     var person = new Person
        //     {
        //         Name = "Wallace",
        //         Age = 30,
        //         HomeAddress = new Address
        //         {
        //             Street = "Rua Exemplo",
        //             City = "São Paulo",
        //             Country = "Brasil"
        //         },
        //         PreviousAddresses =
        //         [
        //             new Address { Street = "Antiga 1", City = "Rio", Country = "Brasil" },
        //             new Address { Street = "Antiga 2", City = "BH", Country = "Brasil" }
        //         ]
        //     };
        //     
        //     var listPerson = new List<Person>()
        //     {
        //         new Person
        //         {
        //             Name = "Wallace",
        //             Age = 30,
        //             HomeAddress = new Address
        //             {
        //                 Street = "Rua Exemplo",
        //                 City = "São Paulo",
        //                 Country = "Brasil"
        //             },
        //             PreviousAddresses =
        //             [
        //                 new Address { Street = "Antiga 1", City = "Rio", Country = "Brasil" },
        //                 new Address { Street = "Antiga 2", City = "BH", Country = "Brasil" }
        //             ]
        //         },
        //         new Person
        //         {
        //             Name = "Wallace2",
        //             Age = 27,
        //             HomeAddress = new Address
        //             {
        //                 Street = "Rua Exemplo",
        //                 City = "São Paulo",
        //                 Country = "Brasil"
        //             },
        //             PreviousAddresses =
        //             [
        //                 new Address { Street = "Antiga 1", City = "Rio", Country = "Brasil" },
        //                 new Address { Street = "Antiga 2", City = "BH", Country = "Brasil" }
        //             ]
        //         }
        //     };
        //     
        //     var listSimplePerson = new List<Person>()
        //     {
        //         new Person
        //         {
        //             Name = "Wallace",
        //             Age = 30,
        //         },
        //         new Person
        //         {
        //             Name = "Wallace2",
        //             Age = 27,
        //         }
        //     };
        //
        //     var mapped = _mapper.Map<Person, PersonDto>(simplePerson);
        //     var mapped2 = _mapper.Map<Person, PersonDto>(person);
        //     var mapped3 = _mapper.Map<Person, PersonDto>(listPerson);
        //     var mapped4 = _mapper.Map<Person, PersonDto>(listSimplePerson);
        //
        //     Console.WriteLine($"Mapeado com sucesso! {mapped}, {mapped2}, {mapped3}, {mapped4}");
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e);
        //     throw;
        // }
    }
}