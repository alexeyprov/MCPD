namespace Cuboid.Modules.BasicMvvm.Models
{
    public class Person
    {
        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format(
                "{1}, {0}",
                FirstName,
                LastName);
        }
    }
}
