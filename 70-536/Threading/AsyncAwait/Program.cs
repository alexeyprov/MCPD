using System;
using System.Threading.Tasks;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine(GetCompositeResultAsync().Result);
    }

    private static async Task<string> GetCompositeResultAsync()
    {
        Task<string> firstTask = GetFirstResultAsync();
        Task<DateTime> secondTask = GetSecondResultAsync();

        await Task.WhenAll(firstTask, secondTask);

        return $"{firstTask.Result} at {secondTask.Result}";
    }

    private static Task<string> GetFirstResultAsync()
    {
        return Task.FromResult(Environment.MachineName);
    }

    private static Task<DateTime> GetSecondResultAsync()
    {
        return Task.FromResult(DateTime.Now);
    }
}