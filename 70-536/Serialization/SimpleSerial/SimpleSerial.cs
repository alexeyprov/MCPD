using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

[Serializable]
public class Employee : 
    IEquatable<Employee>,
    IDeserializationCallback
{
    int _age;
    string _name;
    [NonSerialized]
    int _yb;
    
    public Employee(string name, int age)
    {
        _age = age;
        _name = name;
        RecalcYearOfBirth();
    }

    public string Name
    {
        get
        {
            return _name;
        }
    }

    public int Age
    {
        get
        {
            return _age;
        }
    }

    public int YearOfBirth
    {
        get
        {
            return _yb;
        }
    }

    public bool Equals(Employee e)
    {
        if (null == e)
        {
            return false;
        }
        return (this.Name == e.Name && this.Age == e.Age && 
            this.YearOfBirth == e.YearOfBirth);
    }

    void IDeserializationCallback.OnDeserialization(object caller)
    {
        RecalcYearOfBirth();
    }

    private void RecalcYearOfBirth()
    {
        _yb = DateTime.Now.Year - _age;
    }
}

public class SimpleSerialApp
{
    static void Main()
    {
        TestSerialization(new BinaryFormatter(), "Employee.bin");
        TestSerialization(new SoapFormatter(), "Employee.soap");
    }

    static void TestSerialization(IFormatter formatter, string fileName)
    {
        Employee e = new Employee("Jeffrey Richter", 40);
        using (FileStream writeFs = new FileStream(fileName, FileMode.Create,
            FileAccess.Write))
        {
            formatter.Serialize(writeFs, e);
        }
        Employee restored;
        using (FileStream readFs = new FileStream(fileName, FileMode.Open,
            FileAccess.Read))
        {
            restored = (Employee) formatter.Deserialize(readFs);
        }
        Console.WriteLine("Serialization test with {0} is {1}",
            formatter.GetType(),
            e.Equals(restored) ? "OK" : "FAILED");
    }
}