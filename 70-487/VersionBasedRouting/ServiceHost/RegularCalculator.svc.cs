using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VersionBasedRouting.Contracts;

namespace VersionBasedRouting.ServiceHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RegularCalculator" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RegularCalculator.svc or RegularCalculator.svc.cs at the Solution Explorer and start debugging.
    public class RegularCalculator : ICalculatorService
    {
        double ICalculatorService.Add(double left, double right)
        {
            return left + right;
        }

        double ICalculatorService.Divide(double left, double right)
        {
            return left / right;
        }

        double ICalculatorService.Multiply(double left, double right)
        {
            return left * right;
        }

        double ICalculatorService.Subtract(double left, double right)
        {
            return left - right;
        }
    }
}
