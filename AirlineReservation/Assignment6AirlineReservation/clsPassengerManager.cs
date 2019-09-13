using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment6AirlineReservation
{
    class clsPassengerManager
    {
        /// <summary>
        /// This creates a connection to the database
        /// </summary>
        private static readonly clsDataAccess _data = new clsDataAccess();

        /// <summary>
        /// This is the dataset for the flightID
        /// </summary>
        public static DataSet ds;


        /// <summary>
        /// This is the insert/delete of the passengers firstname
        /// </summary>
        public static string insertFname;

        /// <summary>
        /// This will insert/delete of the pasdsengers lastname
        /// </summary>
        public static string insertLName;

        /// <summary>
        /// This is a total amount of passengers on the flight
        /// </summary>
        public static int totalPassengers;

        /// <summary>
        /// This is the passengers ID
        /// </summary>
        public static string passengerId;


        /// <summary>
        /// This will return a list of Passengers based on flightID
        /// </summary>
        /// <param name="selection">This is the flightID</param>
        /// <returns></returns>
        public static List<Passenger> getPassengers(string selection)
        {
            try
            {
                int iRet = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsAirlineSQL.getInfo(selection), ref iRet); // The DataSet of all the flights
                List<Passenger> flights = new List<Passenger>(); // The list of flights to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    var passenger = new Passenger()
                    {
                        passengerId = row[0].ToString(),
                        passengerfName = row[1].ToString(),
                        passengerlName = row[2].ToString(),
                        passengerSeat = row[3].ToString()
                    };

                    flights.Add(passenger);
                }
                    return flights;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<Passenger>();
            }
        }


        /// <summary>
        /// This will set the dataset equal to all of the passengers on that flight
        /// </summary>
        /// <param name="flightId">The selected flightID</param>
        public static void getPassengersByFlightId(string flightId)
        {
            try
            {
                int iRet = 0; // Temp ref integer, unused but required
                totalPassengers = iRet;
                ds = _data.ExecuteSQLStatement(clsAirlineSQL.getInfo(flightId), ref iRet); // The DataSet of all the passengers
                List<Passenger> flights = new List<Passenger>(); // The list of flights to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    var passenger = new Passenger()
                    {
                        passengerId = row[0].ToString(),
                        passengerfName = row[1].ToString(),
                        passengerlName = row[2].ToString(),
                        passengerSeat = row[3].ToString()
                    };

                    flights.Add(passenger);
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
        /// Thist will get the passegengers names
        /// </summary>
        /// <returns>List of passengers.</returns>
        public static List<string> getPassenger()
        {
            try
            {
                List<string> passengers = new List<string>(); // The list of items to be returned
                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual item) in the ItemDesc table
                    passengers.Add(row[1].ToString() +' ' + row[2].ToString());
                return passengers;
            }
            catch
            {
                throw new Exception("Could not get all items");
            }
        }

        /// <summary>
        /// This will get all the seats that are taken
        /// </summary>
        /// <returns>All of the seats that are taken</returns>
        public static List<string> getPassengerSeats()
        {
            try
            {
                List<string> seats = new List<string>(); // The list of items to be returned
                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual item) in the ItemDesc table
                    seats.Add(row[3].ToString());
                return seats;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<string>();
            }
        }

        /// <summary>
        /// This will get the passengers seatnumber they are in right now
        /// </summary>
        /// <param name="firstName">First Name of Passenger</param>
        /// <param name="lastName">Last Name of Passenger</param>
        /// <returns></returns>
        public static int getSeatNum(string firstName, string lastName)
        {
            try
            {
                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual item) in the ItemDesc table
                    if (row[1].ToString() == firstName && row[2].ToString() == lastName)
                    {
                        return int.Parse(row[3].ToString());
                    }
                return 0;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// This will get the pasengerID
        /// </summary>
        /// <param name="firstName">User's firstnMae</param>
        /// <param name="lastName">User's lastName</param>
        /// <returns>The PassengerID</returns>
        public static int getPassengerId(string firstName, string lastName)
        {
            try
            {
                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual item) in the ItemDesc table
                    if (row[1].ToString() == firstName && row[2].ToString() == lastName)
                    {
                        return int.Parse(row[3].ToString());
                    }
                return 0;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// This will add a passenger to the link table
        /// </summary>
        /// <param name="flightID">This is the flightID they want</param>
        /// <param name="insertFname">This is their firstName</param>
        /// <param name="insertLName">This is their lastName</param>
        /// <param name="seat">This is the seatnumber they want</param>
        public static void addPassenger(string flightID, string insertFname, string insertLName, string seat)
        {
            try
            {
                _data.ExecuteNonQuery(clsAirlineSQL.insertPassenger(insertFname, insertLName));
                int iRet = 0;
                DataSet tmp = _data.ExecuteSQLStatement(clsAirlineSQL.selectPassengerID(insertFname, insertLName), ref iRet); // The DataSet of all the flights
                List<Passenger> passId = new List<Passenger>(); // The list of flights to be returned


                foreach (DataRow row in tmp.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    passengerId = row[0].ToString();
                }


                _data.ExecuteNonQuery(clsAirlineSQL.InsertPassengerLink(flightID, passengerId, seat));
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return;
            }
        }

        /// <summary>
        /// This will update the passenger based on the info the user selected
        /// </summary>
        /// <param name="flightID">FlightId the user has</param>
        /// <param name="insertFname">The users name</param>
        /// <param name="insertLName">The users LastName</param>
        /// <param name="newSeat">Their new seat number</param>
        public static void updatePassenger(string flightID, string insertFname, string insertLName, string newSeat)
        {
            try
            {
                int iRet = 0;
                DataSet tmp = _data.ExecuteSQLStatement(clsAirlineSQL.selectPassengerID(insertFname, insertLName), ref iRet); // The DataSet of all the flights
                List<Passenger> passId = new List<Passenger>(); // The list of flights to be returned


                foreach (DataRow row in tmp.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    passengerId = row[0].ToString();
                }

                _data.ExecuteNonQuery(clsAirlineSQL.updatePassengerLink(newSeat, flightID, passengerId));
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return;
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

    public class Passenger
    {
        public string passengerId { get; set; }
        public string passengerfName { get; set; }
        public string passengerlName { get; set; }
        public string passengerSeat { get; set; }
    }
}
