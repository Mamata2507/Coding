using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    class clsFlightManager
    {
        /// <summary>
        /// This will be the dataset for the flights
        /// </summary>
        public static DataSet ds;

        /// <summary>
        /// This will determine how many flights there is in the system
        /// </summary>
        public static int totalFlights;

        /// <summary>
        /// This creates a connection to the database
        /// </summary>
        private static readonly clsDataAccess _data = new clsDataAccess();

        /// <summary>
        /// This will get all of the flights intialized in the class
        /// </summary>
        public static void getAllFlights()
        {
            try
            {
                int iRet = 0; // Temp ref integer, unused but required
                totalFlights = iRet;
                ds = _data.ExecuteSQLStatement(clsAirlineSQL.sSQL, ref iRet); // The DataSet of all the flights
                List<Flight> flights = new List<Flight>(); // The list of flights to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    //Flight_ID, Flight_Number, Aircraft_Type
                    var flight = new Flight() // Temp invoice object to be added to the list
                    {
                        FlightId = row[0].ToString(),
                        FlightNum = row[1].ToString(),
                        airCraftType = row[2].ToString()
                    };

                    flights.Add(flight);
                }
                return;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return;
            }
        }


        /// <summary>
        /// This will return a list of all the flights
        /// </summary>
        /// <returns></returns>
        public static List<string> getAllFlightsNames()
        {
            try
            {
                List<string> items = new List<string>(); // The list of items to be returned
                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual item) in the ItemDesc table
                    items.Add(row[0].ToString());
                return items;
            }
            catch
            {
                throw new Exception("Could not get all items");
            }
        }


    }

    /// <summary>
    /// This is the Flight instance of a class
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// This is the flightID
        /// </summary>
        public string FlightId { get; set; }

        /// <summary>
        /// This is the flightNumber
        /// </summary>
        public string FlightNum { get; set; }

        /// <summary>
        /// This is the aircraft Type
        /// </summary>
        public string airCraftType { get; set; }

    }
}
