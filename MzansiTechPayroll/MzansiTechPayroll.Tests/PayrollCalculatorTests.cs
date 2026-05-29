using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MzansiTechPayroll;


namespace MzansiTechPayroll.Tests
{
    [TestClass]
    public class PayrollCalculatorTests
    {
        private PayrollCalculator calculator;

        [TestInitialize]
        public void Setup()
        {
            calculator = new PayrollCalculator();
        }

        [TestMethod]
        [TestCategory("Payroll Calculations")]
        public void CalculateGrossPay_ValidHours_ReturnsCorrectAmount()
        {
            decimal hours = 20m;
            decimal expected = 19000m;

            decimal actual = calculator.CalculateGrossPay(hours);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("Payroll Calculations")]
        public void CalculateUIF_ValidGrossPay_ReturnsOnePercent()
        {
            decimal grossPay = 19000m;
            decimal expected = 190m;

            decimal actual = calculator.CalculateUIF(grossPay);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("Payroll Calculations")]
        public void CalculateMembershipFee_ValidGrossPay_ReturnsThirteenPercent()
        {
            decimal grossPay = 19000m;
            decimal expected = 2470m;

            decimal actual = calculator.CalculateMembershipFee(grossPay);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // Payroll tax calculation with dependents 
        public void CalculatePAYE_WithDependents_AppliesCorrectFormula()
        {
            decimal grossPay = 19000m;
            int dependents = 1;
            decimal expected = 4476.875m;    

            decimal actual = calculator.CalculatePAYE(grossPay, dependents);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        // Integration Test
        public void CalculatePayroll_FullCalculation_ReturnsCorrectResults()
        {
            decimal hours = 20m;
            int dependents = 1;

            // Expected values based on the calculations:
            PayrollResult result = calculator.CalculatePayroll("Kamo Phaladi", hours, dependents);

            Assert.AreEqual(19000m, result.GrossPay);
            Assert.AreEqual(190m, result.UIF);
            Assert.AreEqual(2470m, result.MembershipFee);
            Assert.AreEqual(4476.875m, result.PAYE);      
            Assert.AreEqual(11863.125m, result.NetPay);   
        }
    }
}