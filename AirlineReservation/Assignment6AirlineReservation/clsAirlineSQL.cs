using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// This will be a class full of sql statements
    /// </summary>
    class clsAirlineSQL
    {
        /// <summary>
        /// This will grab all of the aircrafts
        /// </summary>
        public static string getAllAirCrafts =>
            "SELECT Aircraft_Type "
            + " FROM Flight";

        /// <summary>
        /// This will grab all of the passengers
        /// </summary>
        public static string getAllPassengers =>
            "SELECT DISTINCT First_name + ' ' + Last_Name"
                + " FROM Passenger";

        /// <summary>
        /// This will grab all of the flight info
        /// </summary>
        /// <param name="flight">This tis the flight number they are looking for</param>
        /// <returns>sql statement</returns>
        public static string getInfo(string flight) => "SELECT Passenger.Passenger_ID, First_Name, Last_Name, FPL.Seat_Number " +
                      "FROM Passenger, Flight_Passenger_Link FPL " +
                      "WHERE Passenger.Passenger_ID = FPL.Passenger_ID AND " +
                      "Flight_ID = " + flight;

        /// <summary>
        /// This will grab all of the flight information.
        /// </summary>
        public static string sSQL => "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";

        /// <summary>
        /// This will insert a passenger
        /// </summary>
        /// <param name="firstName">This is the passengers first name</param>
        /// <param name="lastName">This is the passengers last name</param>
        /// <returns>sql statement</returns>
        public static string insertPassenger(string firstName, string lastName) =>
                        "INSERT INTO Passenger(First_Name,Last_Name) VALUES('" + firstName + "', '" + lastName + "')";

        /// <summary>
        /// Returns SQL statement of the passenger id for that name.
        /// </summary>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <returns>sql statement</returns>
        public static string selectPassengerID(string firstName, string lastName) =>
                        "SELECT Passenger_ID FROM Passenger WHERE First_Name = '" + firstName + "' AND Last_Name= '" + lastName + "'";


        /// <summary>
        /// This will delete the passenger from the passenger table
        /// </summary>
        /// <param name="passengerId">Passengers ID</param>
        /// <returns>sql statement</returns>
        public static string deletePassengerIDPassengerTable(int passengerId) =>
                            "DELETE FROM PASSENGER WHERE Passenger_ID = " + passengerId;

        /// <summary>
        /// This will insert a passenger into the link table
        /// </summary>
        /// <param name="flightId">This is the FlightID</param>
        /// <param name="passengerId">This is the PassengerID</param>
        /// <param name="seatNumber">This is the seatNumber they want</param>
        /// <returns></returns>
        public static string InsertPassengerLink(string flightId, string passengerId, string seatNumber) =>
                            "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) VALUES( " + flightId + " , " + passengerId + " ," + seatNumber + " )";

        /// <summary>
        /// This will update the passengers seat
        /// </summary>
        /// <param name="seatNumber">This is the new seat Number</param>
        /// <param name="FlightId">This is the flight number</param>
        /// <param name="passengerId">This is the passengerID</param>
        /// <returns></returns>
        public static string updatePassengerLink(string seatNumber, string FlightId, string passengerId) =>
                        "UPDATE FLIGHT_PASSENGER_LINK SET Seat_Number = " + int.Parse(seatNumber) +
                        " WHERE FLIGHT_ID = " + int.Parse(FlightId) + " AND PASSENGER_ID = " + int.Parse(passengerId);

        /// <summary>
        /// This will delete the passenger from the flight link table
        /// </summary>
        /// <param name="passengerId">sql statement</param>
        /// <returns></returns>
        public static string deletePassengerIDFromFlightLink(int passengerId) =>
                            "DELETE FROM Flight_Passenger_Link WHERE Passenger_ID = " + passengerId;
    }
}
