using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MzansiTechPayroll
    {
        public class PayrollCalculator
        {
            // Constants as per business rules
            private const decimal HOURLY_RATE = 950.00m;      // R950 per hour
        private const decimal UIF_RATE = 0.01m;           // 1%
            private const decimal MEMBERSHIP_RATE = 0.13m;    // 13%

            // Calculates Gross Pay
            public decimal CalculateGrossPay(decimal hoursWorked)
            {
                return hoursWorked * HOURLY_RATE;
            }

            // Calculates UIF Deduction (1% of Gross Pay)
            public decimal CalculateUIF(decimal grossPay)
            {
                return grossPay * UIF_RATE;
            }

            // Calculates Membership Fee (13% of Gross Pay)
            public decimal CalculateMembershipFee(decimal grossPay)
            {
                return grossPay * MEMBERSHIP_RATE;
            }

        // PAYE = (Gross Pay - (Gross Pay × 0.0575 × Dependents)) × 25%
            public decimal CalculatePAYE(decimal grossPay, int dependents)
            {
            decimal rebatePerDependent = 0.0575m;
            decimal taxRebate = grossPay * rebatePerDependent * dependents;
            decimal taxableAmount = grossPay - taxRebate;
            return taxableAmount * 0.25m;
            }

        // Calculates Net Pay
        public decimal CalculateNetPay(decimal grossPay, decimal uif, decimal paye, decimal membership)
            {
                return grossPay - uif - paye - membership;
            }

            // Main method that calculates everything and returns all values
            public PayrollResult CalculatePayroll(string contractorName, decimal hoursWorked, int dependents)
            {
                decimal grossPay = CalculateGrossPay(hoursWorked);
                decimal uif = CalculateUIF(grossPay);
                decimal membership = CalculateMembershipFee(grossPay);
                decimal paye = CalculatePAYE(grossPay, dependents);
                decimal netPay = CalculateNetPay(grossPay, uif, paye, membership);
                decimal totalDeductions = uif + paye + membership;

                return new PayrollResult
                {
                    ContractorName = contractorName,
                    GrossPay = grossPay,
                    UIF = uif,
                    PAYE = paye,
                    MembershipFee = membership,
                    TotalDeductions = totalDeductions,
                    NetPay = netPay
                };
            }
        }

        // Helper class to return all results together
        public class PayrollResult
        {
            public string ContractorName { get; set; }
            public decimal GrossPay { get; set; }
            public decimal UIF { get; set; }
            public decimal PAYE { get; set; }
            public decimal MembershipFee { get; set; }
            public decimal TotalDeductions { get; set; }
            public decimal NetPay { get; set; }
        }
    }

