namespace Pets
{
    public sealed class Cat : IPet
    {
        string IPet.TalkToOwner() => "Meow";
    }
}