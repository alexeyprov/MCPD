using System;

using Pets;
using Xunit;

public class PetTests
{
    [Fact]
    public void DogTalkToOwnerTest()
    {
        string expected = "Woof";
        string actual = ((IPet)(new Dog())).TalkToOwner();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CatTalkToOwnerTest()
    {
        string expected = "Meow";
        string actual = ((IPet)(new Cat())).TalkToOwner();

        Assert.Equal(expected, actual);
    }
}
