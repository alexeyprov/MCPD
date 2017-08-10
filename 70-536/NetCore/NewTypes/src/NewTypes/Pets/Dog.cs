namespace Pets
{
    public sealed class Dog : IPet
    {
        string IPet.TalkToOwner() => "Woof";
    }
}