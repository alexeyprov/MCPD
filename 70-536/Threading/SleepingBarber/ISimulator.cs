using System.Threading.Tasks;

namespace SleepingBarber
{
    internal interface ISimulator
    {
        Task Run(int barberCount, int customerCount, int chairCount);
    }
}