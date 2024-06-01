namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class User
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }
    public bool isAdmin { get; set; }
    public List<Address> address { get; set; }
}

public class Address
{
    public string addressId { get; set; }
    public string address { get; set; }
    public int zip { get; set; }
    public City city { get; set; }
    public State state { get; set; }
    public string country { get; set; }

}

public class City
{
    public string _id { get; set; }
    public string name { get; set; }
    public string state { get; set; }
}

public class State
{
    public string name { get; set; }
    public string code { get; set; }
}
