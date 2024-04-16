
using System.Windows;

namespace StringCheckLibrary
{
    public class CheckStringClass
    {
        public bool SimCardNumberCheck(int simCardNumber)
        {
            string correctSymbols = "1234567890";
            string simCardNumberString = simCardNumber.ToString();
            if (!simCardNumberString.All(x => correctSymbols.Contains(x)))
                throw new Exception("Номер SIM-Карты имеет некоректные символы");
            if (String.IsNullOrWhiteSpace(simCardNumberString))
                throw new Exception("Номер SIM-Карты пустой");
            return true;
        }
    }

}
