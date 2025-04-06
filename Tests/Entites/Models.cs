using AutoMapper.Attributes;

namespace Tests.Entites;

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }

    [IgnoreMap]
    public string Country { get; set; }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Address HomeAddress { get; set; }
    public List<Address> PreviousAddresses { get; set; } = new();
}