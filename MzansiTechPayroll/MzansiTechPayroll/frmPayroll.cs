using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MzansiTechPayroll
{
    public partial class PayrollForm : Form
    {
        // Instance of PayrollCalculator to perform calculations
        private PayrollCalculator calculator = new PayrollCalculator();

        public PayrollForm()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // Getting Input Validation
                if (!ValidateInputs(out string errorMessage))
                {
                    MessageBox.Show(errorMessage, "Input Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Getting input values
                string contractorName = txtContractorName.Text.Trim();
                decimal hoursWorked = decimal.Parse(txtHoursWorked.Text);
                int dependents = int.Parse(txtDependents.Text);

                // Getting Calculate using our class
                PayrollResult result = calculator.CalculatePayroll(
                    contractorName, hoursWorked, dependents);

                // Getting Display results
                DisplayResults(result);

                // Show success message
                MessageBox.Show("Payroll calculated successfully!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs(out string errorMessage)
        {
            errorMessage = string.Empty;

            // Contractor Name
            if (string.IsNullOrWhiteSpace(txtContractorName.Text))
            {
                errorMessage = "Contractor Name cannot be empty.";
                return false;
            }

            // Hours Worked
            if (!decimal.TryParse(txtHoursWorked.Text, out decimal hours) || hours <= 0)
            {
                errorMessage = "Hours Worked must be a positive number.";
                return false;
            }

            // Number of Dependents
            if (!int.TryParse(txtDependents.Text, out int dependents) ||
                dependents < 0 || dependents > 10)
            {
                errorMessage = "Number of Dependents must be between 0 and 10.";
                return false;
            }

            return true;
        }

        private void DisplayResults(PayrollResult result)
        {
            // Displaying results in the text boxes
            txtGrossPay.Text = result.GrossPay.ToString("C2");
            txtUIF.Text = result.UIF.ToString("C2");
            txtPAYE.Text = result.PAYE.ToString("C2");
            txtMembership.Text = result.MembershipFee.ToString("C2");
            txtTotalDeductions.Text = result.TotalDeductions.ToString("C2");
            txtNetPay.Text = result.NetPay.ToString("C2");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Clearing all input and output fields
            txtContractorName.Clear();
            txtHoursWorked.Clear();
            txtDependents.Clear();

            txtGrossPay.Clear();
            txtUIF.Clear();
            txtPAYE.Clear();
            txtMembership.Clear();
            txtTotalDeductions.Clear();
            txtNetPay.Clear();

            txtContractorName.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Confirm before exiting
            var result = MessageBox.Show("Are you sure you want to exit?",
                "Exit Application",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}