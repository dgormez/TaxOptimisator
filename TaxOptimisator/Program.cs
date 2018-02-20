using System;
using System.Collections.Generic;

namespace TaxOptimisator
{
    class Program
    {
        static void Main(string[] args)
        {
            double managerSalary = 36000;
            double taxableBaseBeforeManagerSalary = 75000;

            int optimalComplementorySalary = 0;
            double optimalTotalTaxPhysicalAndCorporate = taxableBaseBeforeManagerSalary;
            FiscalMetrics fiscal;

            for (int complementOnSalary = (int) managerSalary * -1; complementOnSalary < taxableBaseBeforeManagerSalary - managerSalary; complementOnSalary++)
            {
                fiscal = new FiscalMetrics(taxableBaseBeforeManagerSalary, managerSalary, complementOnSalary);

                if (fiscal.CombinedPhysicalAndCorporateTaxes() < optimalTotalTaxPhysicalAndCorporate)
                {
                    optimalTotalTaxPhysicalAndCorporate = fiscal.CombinedPhysicalAndCorporateTaxes();
                    optimalComplementorySalary = complementOnSalary;
                }

            }

            fiscal = new FiscalMetrics(taxableBaseBeforeManagerSalary, managerSalary, optimalComplementorySalary);
            fiscal.print();

            Console.WriteLine(string.Format("Optimal Combined Tax Physical And Corporate = {0}", optimalTotalTaxPhysicalAndCorporate));
            

            Console.ReadLine();
        }

    }

}
