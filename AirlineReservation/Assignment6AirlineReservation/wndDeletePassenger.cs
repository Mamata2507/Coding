using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// This is an isntance window for delting a passenger
    /// </summary>
    public partial class wndDeletePassenger: Form
    {
        /// <summary>
        /// This will be the passengers first name
        /// </summary>
        private string fName;

        /// <summary>
        /// This will be the passengers last name
        /// </summary>
        private string lName;

        /// <summary>
        /// This will be the passengers ID number in the database
        /// </summary>
        int passengerId;

        /// <summary>
        /// This will be the database connection
        /// </summary>
        private static readonly clsDataAccess _data = new clsDataAccess();

        /// <summary>
        /// This is a constructor for the window
        /// </summary>
        public wndDeletePassenger()
        {
            try
            {
                InitializeComponent();
                fName = "";
                lName = "";
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This will be an overloaded constructor for the window
        /// </summary>
        /// <param name="fName">This is the passengers first name</param>
        /// <param name="lName">This is the passengers last name</param>
        public wndDeletePassenger(string fName, string lName)
        {
            try
            {
                InitializeComponent();
                this.fName = fName;
                this.lName = lName;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This will just close the window if the user wants to
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This will delete the passenger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                DataSet ds = _data.ExecuteSQLStatement(clsAirlineSQL.selectPassengerID(fName, lName), ref i); // The DataSet of all the flights

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    passengerId = int.Parse(row[0].ToString());
                }

                //Delete the passenger flight info
                _data.ExecuteNonQuery(clsAirlineSQL.deletePassengerIDFromFlightLink(passengerId));
                //Delete the passenger
                _data.ExecuteNonQuery(clsAirlineSQL.deletePassengerIDPassengerTable(passengerId));
                fName = "";
                lName = "";
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This happens whenever there is an error
        /// </summary>
        /// <param name="sClass">This is the class it happened in</param>
        /// <param name="sMethod">This is the method</param>
        /// <param name="sMessage">The message that was given</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
