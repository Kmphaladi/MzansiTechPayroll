using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MzansiTechPayroll
    {
        public class PayrollCalculator
        {
            private const decimal HourlyRate = 950.00m;
            private const decimal UIFRate = 0.01m;           // 1%
            private const decimal MembershipRate = 0.13m;    // 13%

            public decimal CalculateGrossPay(decimal hoursWorked)
            {
                return hoursWorked * HourlyRate;
            }

            public decimal CalculateUIF(decimal grossPay)
            {
                return grossPay * UIFRate;
            }

            public decimal CalculateMembershipFee(decimal grossPay)
            {
                return grossPay * MembershipRate;
            }

            public decimal CalculatePAYE(decimal grossPay, int dependents)
            {
                decimal taxCredit = grossPay * 0.0575m * dependents;
                decimal taxableAmount = grossPay - taxCredit;
                return taxableAmount * 0.25m;   // 25%
            }

            public decimal CalculateNetPay(decimal grossPay, decimal uif, decimal paye, decimal membership)
            {
                return grossPay - uif - paye - membership;
            }

            public decimal CalculateTotalDeductions(decimal uif, decimal paye, decimal membership)
            {
                return uif + paye + membership;
            }
        }
    }

