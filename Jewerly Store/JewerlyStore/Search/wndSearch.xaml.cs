using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace CS3280_FinalProject
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : MetroWindow
    {

        /// <summary>
        /// This will intitalize the screen
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();
                InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoices();
                invoice.ItemsSource = clsSearchLogic.GetAllInvoicesNums();
                dateOptions.ItemsSource = clsSearchLogic.GetAllInvoicesDates();
                total.ItemsSource = clsSearchLogic.GetAllInvoicesTotals();
            }
            catch(Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// When the user clicks cancel it will close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            try {
                this.Close();
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// When the user changes the selection we will query a different result for the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Invoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Verify the users input is not the blank field
                if (invoice.SelectedIndex >= 0)
                {
                    //Search on date and total and invoice
                    if (dateOptions.SelectedIndex >= 0 && total.SelectedIndex >= 0)
                    {
                        InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByEverything(invoice.SelectedItem.ToString(), dateOptions.SelectedItem.ToString(), total.SelectedItem.ToString());
                    }
                    else if (dateOptions.SelectedIndex >= 0 || total.SelectedIndex >= 0)
                    {
                        if (dateOptions.SelectedIndex >= 0)
                        {
                            InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByInvoiceAndDate(invoice.SelectedItem.ToString(), dateOptions.SelectedItem.ToString());
                        }
                        else
                        {
                            InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByTotalAndInvoice(total.SelectedItem.ToString(), invoice.SelectedItem.ToString());
                        }
                    }
                    else
                    {
                        InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByInvoice(invoice.SelectedItem.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// This will change the result of the invoices if there user selects a datef.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dateOptions.SelectedIndex >= 0)
                {
                    //Verify the users input is not the blank field
                    if (invoice.SelectedIndex >= 0 && total.SelectedIndex >= 0)
                    {
                        InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByEverything(invoice.SelectedItem.ToString(), dateOptions.SelectedItem.ToString(), total.SelectedItem.ToString());
                    }
                    else if (invoice.SelectedIndex >= 0 || total.SelectedIndex >= 0)
                    {
                        if (invoice.SelectedIndex >= 0)
                        {
                            InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByInvoiceAndDate(invoice.SelectedItem.ToString(), dateOptions.SelectedItem.ToString());
                        }
                        else
                        {
                            InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByTotalAndDate(total.SelectedItem.ToString(), dateOptions.SelectedItem.ToString());
                        }
                    }
                    else
                    {
                        InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByDate(DateTime.Parse(dateOptions.SelectedItem.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// This will change the datagrid if the user changes the default date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Total_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Verify the users input is not the blank field
                if (total.SelectedIndex > 0)
                {
                    //Verify the users input is not the blank field
                    if (invoice.SelectedIndex > 0 && dateOptions.SelectedIndex > 0)
                    {
                        InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByEverything(invoice.SelectedItem.ToString(), dateOptions.SelectedItem.ToString(), total.SelectedItem.ToString());
                    }
                    else if (dateOptions.SelectedIndex > 0 || invoice.SelectedIndex > 0)
                    {
                        if (invoice.SelectedIndex > 0)
                        {
                            InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByTotalAndInvoice(total.SelectedItem.ToString(), invoice.SelectedItem.ToString());
                        }
                        else
                        {
                            InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByTotalAndDate(total.SelectedItem.ToString(), dateOptions.SelectedItem.ToString());
                        }
                    }
                    else
                    {
                        InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoicesByTotal(total.SelectedItem.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
        
        public string ShowWindow()
        {
            try
            {
                this.ShowDialog();
                Invoice item = (Invoice)InvoiceDataGrid.SelectedItem;
                return item?.InvoiceNum ?? "";
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return "";
            }
        }
        
        /// <summary>
        /// This will reset the searh forms if the user clicks the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                invoice.SelectedIndex = -1;
                dateOptions.SelectedIndex = -1;
                total.SelectedIndex = -1;
                InvoiceDataGrid.ItemsSource = clsSearchLogic.GetAllInvoices();
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// This will submit the invoice the user is looking for.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
