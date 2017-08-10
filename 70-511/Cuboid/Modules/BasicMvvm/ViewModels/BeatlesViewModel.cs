using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

using Cuboid.Modules.BasicMvvm.Models;
using Prism.Commands;

namespace Cuboid.Modules.BasicMvvm.ViewModels
{
    public sealed class BeatlesViewModel
    {
        public ICommand TestCommand
        {
            get
            {
                return new DelegateCommand<Person>(DoTestCommand);
            }
        }

        public string Greeting
        {
            get
            {
                return "Hello from MVVM";
            }
        }

        public string ViewName
        {
            get
            {
                return "Beatles Grid";
            }
        }

        public IEnumerable<Person> Beatles
        {
            get
            {
                return new[]
                {
                    new Person
                    {
                        FirstName = "John",
                        LastName = "Lennon"
                    },
                    new Person
                    {
                        FirstName = "Paul",
                        LastName = "McCartney"
                    },
                    new Person
                    {
                        FirstName = "George",
                        LastName = "Harrison"
                    },
                    new Person
                    {
                        FirstName = "Ringo",
                        LastName = "Starr"
                    }
                };
            }
        }

        private void DoTestCommand(Person person)
        {
            Debug.WriteLine("Test command executed on {0}", person);
        }
    }
}
