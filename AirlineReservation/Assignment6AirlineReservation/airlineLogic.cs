using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// This will be the backend of the airline main screen logic
    /// </summary>
    class airlineLogic
    {
        /// <summary>
        /// This creates a connection to the database
        /// </summary>
        private static readonly clsDataAccess _data = new clsDataAccess();

        /// <summary>
        /// This will get all of the flights
        /// </summary>
        /// <returns>A list of all of the flights</returns>
        public static List<string> getAllFlights()
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsAirlineSQL.getAllAirCrafts, ref i); // The DataSet of all the flights
                List<string> flights = new List<string>(); // The list of flights to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    flights.Add(row[0].ToString());
                }

                return flights;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<string>();
            }
        }

        /// <summary>
        /// This will get a list of all passengers
        /// </summary>
        /// <returns>List of all of the passengers</returns>
        public static List<string> getAllPassengers()
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsAirlineSQL.getAllPassengers, ref i); // The DataSet of all the invoices
                List<string> passengers = new List<string>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    passengers.Add(row[0].ToString());
                }

                return passengers;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<string>();
            }
        }


        /// <summary>
        /// The invoice class, used to hold an invoice object from the database 
        /// This is used both in this file and in the wndMain.xaml.cs file
        /// </summary>
        public class Flight
        {
            /// <summary>
            /// The invoice num
            /// </summary>
            public string Flight_Num { get; set; }
            /// <summary>
            /// The date of the invoice
            /// </summary>
            public string Passenger_ID { get; set; }
        }

    }
}
