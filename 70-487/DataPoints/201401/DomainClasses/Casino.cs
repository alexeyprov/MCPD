using System;
using System.Collections.Generic;
using DomainClasses.Infrastructure;


namespace DomainClasses
{
public class Casino
{
  public Casino()
  {
    SlotMachines = new List<SlotMachine>();
  }
  public int Id { get; set; }
  public string Name { get; set; }
  public CasinoRating Rating { get; set; }
  public String Description { get; set; }
  public ICollection<SlotMachine> SlotMachines { get; set; }
  public Address PhysicalAddress { get; set; }
  public Address MailingAddress { get; set; }
  public DateTime OpeningDate { get; set; }
  public String SomeNewProperty { get; set; }
  public String SomeOtherNewProperty { get; set; }
  public String AgainSomeNewProperty { get; set; }
 
  
  public class Address:ValueObject<Address>
  {
    protected Address()
    {
      
    }
    public Address(string streetOrPoBox, string city, string state,string postalCode)
    {
      City = city;
      State = state;
      PostalCode = postalCode;
      StreetOrPoBox = streetOrPoBox;
    }

    public string StreetOrPoBox { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PostalCode { get; private set; }
  }

}

  public enum CasinoRating
  {
    Meh = 1,
    Nice = 2,
    JustLikeonTv = 3
  }
}
