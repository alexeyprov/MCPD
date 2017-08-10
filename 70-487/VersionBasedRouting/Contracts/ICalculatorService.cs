using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace VersionBasedRouting.Contracts
{
    [ServiceContract(Name="CalculatorService", Namespace= "http://alexeypr.com/2015/09/VersionBasedRouting")]
    public interface ICalculatorService
    {
        [OperationContract]
        double Add(double left, double right);

        [OperationContract]
        double Subtract(double left, double right);

        [OperationContract]
        double Multiply(double left, double right);

        [OperationContract]
        double Divide(double left, double right);
    }
}
