// This interface is common for all strong-typed employee collections
public interface IEmployeeGetter
{
	Employee this[int index] { get; }
}