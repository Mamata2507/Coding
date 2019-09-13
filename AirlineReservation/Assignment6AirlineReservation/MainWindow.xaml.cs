using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// This will allow us to have a window instance of delete passneger
        /// </summary>
        wndAddPassenger wndAddPass;

        /// <summary>
        /// Delete passenger instance
        /// </summary>
        public wndDeletePassenger wndDeletePass { get; private set; }


        /// <summary>
        /// This will be able to detect if they are changing seats
        /// </summary>
        public Boolean changingSeats = false;

        /// <summary>
        /// This will create instance of the main window
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                clsFlightManager.getAllFlights();
                cbChooseFlight.ItemsSource = clsFlightManager.getAllFlightsNames();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This will happen whenver the flight option has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbChooseFlight.SelectedItem.ToString() == "1")
                {
                    loadPassengers();
                    lblPassengersSeatNumber.Content = "";
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                    gPassengerCommands.IsEnabled = true;
                    cbChoosePassenger.IsEnabled = true;
                    cmdDeletePassenger.IsEnabled = false;
                    cmdChangeSeat.IsEnabled = false;
                }
                else if (cbChooseFlight.SelectedItem.ToString() == "2")
                {
                    loadPassengers();
                    lblPassengersSeatNumber.Content = "";
                    CanvasA380.Visibility = Visibility.Visible;
                    Canvas767.Visibility = Visibility.Hidden;
                    gPassengerCommands.IsEnabled = true;
                    cbChoosePassenger.IsEnabled = true;
                    cmdDeletePassenger.IsEnabled = false;
                    cmdChangeSeat.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// This will laod the apsdsengers based on the flightID and intilize the new passengers/oldpassengers
        /// </summary>
        private void loadPassengers()
        {
            try
            {
                string flightId = cbChooseFlight.SelectedItem.ToString();

                if (flightId == "1")
                {
                    clsPassengerManager.getPassengersByFlightId(flightId);
                    cbChoosePassenger.ItemsSource = clsPassengerManager.getPassenger();
                    update767Seats();
                }
                else if (flightId == "2")
                {
                    clsPassengerManager.getPassengersByFlightId(flightId);
                    cbChoosePassenger.ItemsSource = clsPassengerManager.getPassenger();
                    updateA380Seats();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This will update the seats for each label based on the flight
        /// </summary>
        private void update767Seats()
        {
            try
            {
                List<String> seatNums;
                seatNums = clsPassengerManager.getPassengerSeats();

                if (cbChoosePassenger.SelectedIndex < 0)
                {
                    foreach (Label lblSeat in c767_Seats.Children)
                    {
                        if (seatNums.Contains(lblSeat.Content))
                        {
                            lblSeat.Background = Brushes.Red;
                        }
                        else
                        {
                            lblSeat.Background = Brushes.Blue;
                        }
                    }
                }
                else
                {
                    string[] words = cbChoosePassenger.SelectedItem.ToString().Split(' ');

                    int seatNum = clsPassengerManager.getSeatNum(words[0], words[1]);

                    foreach (Label lblSeat in c767_Seats.Children)
                    {
                        if (lblSeat.Content.ToString() == seatNum.ToString())
                        {
                            lblSeat.Background = Brushes.Green;
                            lblPassengersSeatNumber.Content = seatNum.ToString();
                        }
                        else if (seatNums.Contains(lblSeat.Content))
                        {
                            lblSeat.Background = Brushes.Red;
                        }
                        else
                        {
                            lblSeat.Background = Brushes.Blue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This will allow the user to change seats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="e"></param>
        private void CmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int passengerid = 0;
                int seatnum = 0;
                gPassengerCommands.IsEnabled = false;
                string[] words = cbChoosePassenger.SelectedItem.ToString().Split(' ');
                passengerid = clsPassengerManager.getPassengerId(words[0], words[1]);
                seatnum = clsPassengerManager.getSeatNum(words[0], words[1]);
                clsPassengerManager.insertFname = words[0];
                clsPassengerManager.insertLName = words[1];
                markSeatOpen(seatnum);
                changingSeats = true;
                gPassengerCommands.IsEnabled = false;
                cbChooseFlight.IsEnabled = false;
                cbChoosePassenger.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This will mark the seat as open that the passenger is coming from.
        /// </summary>
        /// <param name="seatnum">This is the old seat of the passenger</param>
        private void markSeatOpen(int seatnum)
        {
            try
            {
                //This will determine the flight they are coming from
                if (cbChooseFlight.SelectedItem.ToString() == "1")
                {
                    foreach (Label lblSeat in c767_Seats.Children)
                    {
                        if (lblSeat.Content.ToString() == seatnum.ToString())
                        {
                            lblSeat.Background = Brushes.Blue;
                        }
                    }
                }
                else
                {
                    foreach (Label lblSeat in cA380_Seats.Children)
                    {
                        if (lblSeat.Content.ToString() == seatnum.ToString())
                        {
                            lblSeat.Background = Brushes.Blue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// This will happen whenever AddPassenger has happened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();
                if (clsPassengerManager.insertLName != "" && clsPassengerManager.insertFname != "")
                {
                    gPassengerCommands.IsEnabled = false;
                    cbChooseFlight.IsEnabled = false;
                    cbChoosePassenger.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Anytime a selection has changed when there is a selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbChoosePassenger.SelectedIndex > -1)
                {
                    if (cbChooseFlight.SelectedItem.ToString() == "1")
                    {
                        update767Seats();
                    }
                    else
                    {
                        updateA380Seats();
                    }
                    cmdDeletePassenger.IsEnabled = true;
                    cmdChangeSeat.IsEnabled = true;
                }
                else
                {
                    cmdDeletePassenger.IsEnabled = false;
                    cmdChangeSeat.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This will update the seats for the A380 flight
        /// </summary>
        private void updateA380Seats()
        {
            try
            {
                List<String> seatNums;
                seatNums = clsPassengerManager.getPassengerSeats();

                if (cbChoosePassenger.SelectedIndex < 0)
                {
                    foreach (Label lblSeat in cA380_Seats.Children)
                    {
                        if (seatNums.Contains(lblSeat.Content))
                        {
                            lblSeat.Background = Brushes.Red;
                        }
                        else
                        {
                            lblSeat.Background = Brushes.Blue;
                        }
                    }
                }
                else
                {
                    string[] words = cbChoosePassenger.SelectedItem.ToString().Split(' ');

                    int seatNum = clsPassengerManager.getSeatNum(words[0], words[1]);

                    foreach (Label lblSeat in cA380_Seats.Children)
                    {
                        if (lblSeat.Content.ToString() == seatNum.ToString())
                        {
                            lblSeat.Background = Brushes.Green;
                            lblPassengersSeatNumber.Content = seatNum.ToString();
                        }
                        else if (seatNums.Contains(lblSeat.Content))
                        {
                            lblSeat.Background = Brushes.Red;
                        }
                        else
                        {
                            lblSeat.Background = Brushes.Blue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This will happen whenever there is a seat click event
        /// </summary>
        /// <param name="sender">This is the object that was clicked</param>
        /// <param name="e"></param>
        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Label label = (Label)sender;
                if (changingSeats && gPassengerCommands.IsEnabled == false && (clsPassengerManager.insertFname != "" && clsPassengerManager.insertLName != "") && label.Background == Brushes.Blue)
                {
                    clsPassengerManager.updatePassenger(cbChooseFlight.SelectedItem.ToString(), clsPassengerManager.insertFname, clsPassengerManager.insertLName, label.Content.ToString());
                    gPassengerCommands.IsEnabled = true;
                    loadPassengers();
                    clsPassengerManager.insertFname = "";
                    clsPassengerManager.insertLName = "";
                    gPassengerCommands.IsEnabled = true;
                    cbChooseFlight.IsEnabled = true;
                    cbChoosePassenger.IsEnabled = true;
                    changingSeats = false;
                }
                else if (gPassengerCommands.IsEnabled == false && (clsPassengerManager.insertFname != "" && clsPassengerManager.insertLName != "") && label.Background == Brushes.Blue)
                {
                    clsPassengerManager.addPassenger(cbChooseFlight.SelectedItem.ToString(), clsPassengerManager.insertFname, clsPassengerManager.insertLName, label.Content.ToString());
                    gPassengerCommands.IsEnabled = true;
                    loadPassengers();
                    clsPassengerManager.insertFname = "";
                    clsPassengerManager.insertLName = "";
                    gPassengerCommands.IsEnabled = true;
                    cbChooseFlight.IsEnabled = true;
                    cbChoosePassenger.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// This will happen whenever they waht to delete a passenger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] words = cbChoosePassenger.SelectedItem.ToString().Split(' ');

                wndDeletePass = new wndDeletePassenger(words[0], words[1]);
                wndDeletePass.ShowDialog();
                lblPassengersSeatNumber.Content = "";

                loadPassengers();
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
