namespace Tests.Entites;

public class AddressDto
{
    public string Street { get; set; }
    public string City { get; set; }
}

public class PersonDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public AddressDto HomeAddress { get; set; }
    public List<AddressDto> PreviousAddresses { get; set; }
}