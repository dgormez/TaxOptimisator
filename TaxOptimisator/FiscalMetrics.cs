using System;
using System.Collections.Generic;
using System.Text;

namespace TaxOptimisator
{

    public class FiscalMetrics
    {
        double taxableBaseBeforeManagerSalary;
        double managerSalary;
        double complementOnSalary;

        double directCotisation;
        double taxableBase;
        double taxes;

        public FiscalMetrics(double _taxableBaseBeforeManagerSalary, double _managerSalary, double _complementOnSalary)
        {
            taxableBaseBeforeManagerSalary = _taxableBaseBeforeManagerSalary;
            managerSalary = _managerSalary;
            complementOnSalary = _complementOnSalary;
            directCotisation = DirectCotisation();
            taxableBase = TaxableBase();
            taxes = Taxes();
        }

        public double DirectCotisation()
        {
            directCotisation = 0.05 * ((taxableBaseBeforeManagerSalary / 2) - (managerSalary + complementOnSalary));
            if (directCotisation < 0)
                directCotisation = 0;

            return directCotisation;
        }

        public double TaxableBase()
        {
            taxableBase = taxableBaseBeforeManagerSalary - (managerSalary + complementOnSalary) - directCotisation;
            return taxableBase;
        }

        public double Taxes()
        {
            taxes = (directCotisation > 0) ? taxableBase * 0.2958 : taxableBase * 0.204;
            return taxes;
        }

        public double CorporateTaxes()
        {
            return directCotisation + taxes;
        }

        public double EffectifTaxFactor()
        {
            return (CorporateTaxes() / taxableBase) * 100;
        }
        public double EffectifTotalTaxFactor()
        {
            return (CombinedPhysicalAndCorporateTaxes() / taxableBaseBeforeManagerSalary) * 100;
        }

        public double PhysicalPersonTaxes()
        {
            double totalCorrectedSalary = managerSalary + complementOnSalary;

            List<double> steps = new List<double> { 0, 7270, 11070, 12720, 21190, 38830, int.MaxValue };
            List<double> factors = new List<double> { 0, 0.25, 0.3, 0.4, 0.45, 0.5 };
            List<double> cumuls = new List<double> { 0, 0, 950, 1445, 4833, 12771 };

            for (int i = 0; i < 6; i++)
            {
                if (totalCorrectedSalary >= steps[i] & totalCorrectedSalary < steps[i + 1])
                    return cumuls[i] + (totalCorrectedSalary - steps[i]) * factors[i];
            }

            throw new Exception("Impossible Taxes On Physical Person calculation");
        }

        public double CombinedPhysicalAndCorporateTaxes()
        {
            return PhysicalPersonTaxes() + CorporateTaxes();
        }

        public void print()
        {

            Console.WriteLine(string.Format("Taxable Base Before Manager Salary : {0}", taxableBaseBeforeManagerSalary));
            Console.WriteLine(string.Format("Complementory Salary : {0}", complementOnSalary));
            Console.WriteLine(string.Format("Manager Salary : {0}", managerSalary));

            Console.WriteLine(string.Format("Direct Cotisation : {0}", directCotisation));
            Console.WriteLine(string.Format("Taxable Base : {0}", taxableBase));
            Console.WriteLine(string.Format("Taxes : {0}", taxes));
            Console.WriteLine(string.Format("Corporate Taxes: {0}", CorporateTaxes()));
            Console.WriteLine(string.Format("Physical Person Taxes: {0}", PhysicalPersonTaxes() ));
            Console.WriteLine(string.Format("Total Tax Physical And Corporate : {0}", CombinedPhysicalAndCorporateTaxes()));
            Console.WriteLine(string.Format("Effectif Tax Factor : {0}", EffectifTaxFactor()));
            Console.WriteLine(string.Format("Effectif Total Tax Factor : {0}", EffectifTotalTaxFactor()));
        }
    }
}
