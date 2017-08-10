using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

using Calculator.Interfaces;

namespace SimpleCalculator
{
	internal sealed class Program
	{
		private readonly CompositionContainer _container;

		[Import(typeof(ICalculator))]
		public ICalculator Calculator
		{
			get;
			set;
		}

		public Program()
		{
			ComposablePartCatalog catalog = new AggregateCatalog(
				new AssemblyCatalog(typeof(Program).Assembly),
				new DirectoryCatalog(@"..\..\Extensions"),
				new GenericCatalog());

			_container = new CompositionContainer(catalog);

			try
			{
				_container.ComposeParts(this);
			}
			catch (CompositionException ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		static void Main()
		{
			Program program = new Program();

			Console.Write("Enter an expression: ");

			string input = Console.ReadLine();

			Console.WriteLine("Result: " + program.Calculator.Calculate(input));
		}
	}
}
