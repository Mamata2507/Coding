using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_FinalProject
{
    class clsSearchLogic
    {

        /// <summary>
        /// Access to Database Connection
        /// </summary>
        private static readonly clsDataAccess _data = new clsDataAccess();

        /// <summary>
        /// Gets all the invoices in the database
        /// Creates an Invoice class object and adds to a list
        /// </summary>
        /// <returns>List of all the invoices objects in the database</returns>
        public static List<Invoice> GetAllInvoices()
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoicesData, ref i); // The DataSet of all the invoices
                List<Invoice> invoices = new List<Invoice>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    var invoice = new Invoice() // Temp invoice object to be added to the list
                    {
                        InvoiceNum = row[0].ToString(),
                        InvoiceDate = row[1].ToString().Substring(0, row[1].ToString().IndexOf(' ')),
                        TotalCost = Convert.ToDecimal(row[2]).ToString("c"),
                        Items = ""
                    };

                    DataSet ds1 = _data.ExecuteSQLStatement(clsSearchSQL.GetItemsByInvoiceNum(row[0].ToString()), ref i); // The DataSet of all the items associated with an invoice
                    foreach (DataRow row1 in ds1.Tables[0].Rows) // Each row (each individual item) in the LineItems table
                        invoice.Items += row1[0].ToString() + ", ";
                    invoice.Items = invoice.Items.TrimEnd(' ', ',');
                    invoices.Add(invoice);
                }

                return invoices;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<Invoice>();
            }
        }

        /// <summary>
        /// This will return a list of all of the invoices the user wants based on the invoiceNumber
        /// </summary>
        /// <param name="v">This is the invocie number the user wants</param>
        /// <returns></returns>
        public static List<Invoice> GetAllInvoicesByInvoice(string v)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoicesByInvoice(v), ref i); // The DataSet of all the invoices
                List<Invoice> invoices = new List<Invoice>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    var invoice = new Invoice() // Temp invoice object to be added to the list
                    {
                        InvoiceNum = row[0].ToString(),
                        InvoiceDate = row[1].ToString().Substring(0, row[1].ToString().IndexOf(' ')),
                        TotalCost = Convert.ToDecimal(row[2]).ToString("c"),
                        Items = ""
                    };

                    DataSet ds1 = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoices, ref i); // The DataSet of all the items associated with an invoice
                    foreach (DataRow row1 in ds1.Tables[0].Rows) // Each row (each individual item) in the LineItems table
                        invoice.Items += row1[0].ToString() + ", ";
                    invoice.Items = invoice.Items.TrimEnd(' ', ',');
                    invoices.Add(invoice);
                }

                return invoices;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<Invoice>();
            }
        }

        /// <summary>
        /// This will find all the invoices by the total
        /// </summary>
        /// <param name="v">This is the selection that user made from on the drop down</param>
        /// <returns></returns>
        public static List<Invoice> GetAllInvoicesByTotal(string v)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoicesByTotal(v), ref i); // The DataSet of all the invoices
                List<Invoice> invoices = new List<Invoice>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    var invoice = new Invoice() // Temp invoice object to be added to the list
                    {
                        InvoiceNum = row[0].ToString(),
                        InvoiceDate = row[1].ToString().Substring(0, row[1].ToString().IndexOf(' ')),
                        TotalCost = Convert.ToDecimal(row[2]).ToString("c"),
                        Items = ""
                    };

                    DataSet ds1 = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoices, ref i); // The DataSet of all the items associated with an invoice
                    foreach (DataRow row1 in ds1.Tables[0].Rows) // Each row (each individual item) in the LineItems table
                        invoice.Items += row1[0].ToString() + ", ";
                    invoice.Items = invoice.Items.TrimEnd(' ', ',');
                    invoices.Add(invoice);
                }

                return invoices;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<Invoice>();
            }
        }

        /// <summary>
        /// This will grab all of the invoices by dates
        /// </summary>
        /// <returns>A list of invoices Dateas</returns>
        public static List<DateTime> GetAllInvoicesDates()
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.getAllDates, ref i); // The DataSet of all the invoices
                List<DateTime> dates = new List<DateTime>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                    dates.Add(DateTime.Parse(row[0].ToString()));

                return dates;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<DateTime>();
            }
        }

        /// <summary>
        /// This will grab all of the invoiceNumbers
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllInvoicesNums()
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.getAllInvoiceNums, ref i); // The DataSet of all the invoices
                List<string> invoiceNums = new List<string>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                    invoiceNums.Add(row[0].ToString());

                return invoiceNums;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<string>();
            }
        }

        /// <summary>
        /// This will grab all invoice Totals
        /// </summary>
        /// <returns>A list of invoice totals</returns>
        public static List<string> GetAllInvoicesTotals()
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.getAllTotals, ref i); // The DataSet of all the invoices
                List<string> totals = new List<string>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                    totals.Add(row[0].ToString());

                return totals;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<String>();
            }
        }

        /// <summary>
        /// This will grab all of the invoices by the certain date the user selected
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static List<Invoice> GetAllInvoicesByDate(DateTime v)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoicesByDate(v), ref i); // The DataSet of all the invoices
                List<Invoice> invoices = new List<Invoice>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    var invoice = new Invoice() // Temp invoice object to be added to the list
                    {
                        InvoiceNum = row[0].ToString(),
                        InvoiceDate = row[1].ToString().Substring(0, row[1].ToString().IndexOf(' ')),
                        TotalCost = Convert.ToDecimal(row[2]).ToString("c"),
                        Items = ""
                    };

                    DataSet ds1 = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoices, ref i); // The DataSet of all the items associated with an invoice
                    foreach (DataRow row1 in ds1.Tables[0].Rows) // Each row (each individual item) in the LineItems table
                        invoice.Items += row1[0].ToString() + ", ";
                    invoice.Items = invoice.Items.TrimEnd(' ', ',');
                    invoices.Add(invoice);
                }

                return invoices;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<Invoice>();
            }
        }

        /// <summary>
        /// This will return a list of all the invoices based off of invoice and date
        /// </summary>
        /// <param name="invoice">The invoice the user is looking for</param>
        /// <param name="date">The date the user is looking for</param>
        /// <returns></returns>
        public static List<Invoice> GetAllInvoicesByInvoiceAndDate(string invoice, string date)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoicesByInvoiceAndDate(invoice, date), ref i); // The DataSet of all the invoices
                List<Invoice> invoices = new List<Invoice>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    var newInvoice = new Invoice() // Temp invoice object to be added to the list
                    {
                        InvoiceNum = row[0].ToString(),
                        InvoiceDate = row[1].ToString().Substring(0, row[1].ToString().IndexOf(' ')),
                        TotalCost = Convert.ToDecimal(row[2]).ToString("c"),
                        Items = ""
                    };

                    DataSet ds1 = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoices, ref i); // The DataSet of all the items associated with an invoice
                    foreach (DataRow row1 in ds1.Tables[0].Rows) // Each row (each individual item) in the LineItems table
                        newInvoice.Items += row1[0].ToString() + ", ";
                    newInvoice.Items = newInvoice.Items.TrimEnd(' ', ',');
                    invoices.Add(newInvoice);
                }

                return invoices;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<Invoice>();
            }
        }

        /// <summary>
        /// This will search on all 3 creteria
        /// </summary>
        /// <param name="invoiceNum">This is the invoice number the user wants</param>
        /// <param name="invoiceDate">This is the invoice date the user wants</param>
        /// <param name="invoiceTotal">This is the invoice total the user wants</param>
        /// <returns></returns>
        public static List<Invoice> GetAllInvoicesByEverything(string invoiceNum, string invoiceDate, string invoiceTotal)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoicesByEverything(invoiceNum, invoiceDate, invoiceTotal), ref i); // The DataSet of all the invoices
                List<Invoice> invoices = new List<Invoice>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    var invoice = new Invoice() // Temp invoice object to be added to the list
                    {
                        InvoiceNum = row[0].ToString(),
                        InvoiceDate = row[1].ToString().Substring(0, row[1].ToString().IndexOf(' ')),
                        TotalCost = Convert.ToDecimal(row[2]).ToString("c"),
                        Items = ""
                    };

                    DataSet ds1 = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoices, ref i); // The DataSet of all the items associated with an invoice
                    foreach (DataRow row1 in ds1.Tables[0].Rows) // Each row (each individual item) in the LineItems table
                        invoice.Items += row1[0].ToString() + ", ";
                    invoice.Items = invoice.Items.TrimEnd(' ', ',');
                    invoices.Add(invoice);
                }

                return invoices;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<Invoice>();
            }
        }

        /// <summary>
        /// This will search invoices on total and date
        /// </summary>
        /// <param name="total">The total that the user is looking for</param>
        /// <param name="date">The date the user is looking for</param>
        /// <returns>A list of all of the invoices based on those parameters</returns>
        public static List<Invoice> GetAllInvoicesByTotalAndDate(string total, string date)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoicesByTotalAndDate(total, date), ref i); // The DataSet of all the invoices
                List<Invoice> invoices = new List<Invoice>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    var invoice = new Invoice() // Temp invoice object to be added to the list
                    {
                        InvoiceNum = row[0].ToString(),
                        InvoiceDate = row[1].ToString().Substring(0, row[1].ToString().IndexOf(' ')),
                        TotalCost = Convert.ToDecimal(row[2]).ToString("c"),
                        Items = ""
                    };

                    DataSet ds1 = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoices, ref i); // The DataSet of all the items associated with an invoice
                    foreach (DataRow row1 in ds1.Tables[0].Rows) // Each row (each individual item) in the LineItems table
                        invoice.Items += row1[0].ToString() + ", ";
                    invoice.Items = invoice.Items.TrimEnd(' ', ',');
                    invoices.Add(invoice);
                }

                return invoices;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<Invoice>();
            }
        }

        /// <summary>
        /// This will return all of the invoices by total and invoice
        /// </summary>
        /// <param name="toal">The total that the user is looking for</param>
        /// <param name="invoice">This is the invoice the user is looking for</param>
        /// <returns>A list of all of the invoices based on those parameters</returns>
        public static List<Invoice> GetAllInvoicesByTotalAndInvoice(string total, string invoice)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoicesByTotalAndInvoice(total, invoice), ref i); // The DataSet of all the invoices
                List<Invoice> invoices = new List<Invoice>(); // The list of invoices to be returned

                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual invoice) in the Invoices table
                {
                    var newInvoice = new Invoice() // Temp invoice object to be added to the list
                    {
                        InvoiceNum = row[0].ToString(),
                        InvoiceDate = row[1].ToString().Substring(0, row[1].ToString().IndexOf(' ')),
                        TotalCost = Convert.ToDecimal(row[2]).ToString("c"),
                        Items = ""
                    };

                    DataSet ds1 = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoices, ref i); // The DataSet of all the items associated with an invoice
                    foreach (DataRow row1 in ds1.Tables[0].Rows) // Each row (each individual item) in the LineItems table
                        newInvoice.Items += row1[0].ToString() + ", ";
                    newInvoice.Items = newInvoice.Items.TrimEnd(' ', ',');
                    invoices.Add(newInvoice);
                }

                return invoices;
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
                return new List<Invoice>();
            }
        }
    }
}
