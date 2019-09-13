using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_FinalProject
{
    /// <summary>
    /// The logic for the Main Window
    /// </summary>
    public static class clsMainLogic
    {
        #region Fields

        /// <summary>
        /// The connection to the database
        /// </summary>
        private static readonly clsDataAccess _data = new clsDataAccess();

        #endregion

        #region Public Methods

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
                DataSet ds = _data.ExecuteSQLStatement(clsMainSQL.GetAllInvoices, ref i); // The DataSet of all the invoices
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
                    DataSet ds1 = _data.ExecuteSQLStatement(clsMainSQL.GetItemsByInvoiceNum(row[0].ToString()), ref i); // The DataSet of all the items associated with an invoice
                    foreach (DataRow row1 in ds1.Tables[0].Rows) // Each row (each individual item) in the LineItems table
                        invoice.Items += row1[0].ToString() + ", ";
                    invoice.Items = invoice.Items.TrimEnd(' ', ',');
                    invoices.Add(invoice);
                }

                return invoices;
            }
            catch
            {
                throw new Exception("Could not get all invoices");
            }
        }

        /// <summary>
        /// Gets all the items from the ItemsDesc table
        /// </summary>
        /// <returns>A list of all item names</returns>
        public static List<string> GetAllItems()
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsMainSQL.GetAllItems, ref i); // The DataSet of all the items 
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

        /// <summary>
        /// Gets all the items associated with an invoice num
        /// </summary>
        /// <param name="invoiceNum">The invoice num to be referenced</param>
        /// <returns>A list of all the items associated with an invoice num</returns>
        public static List<string> GetItemsByInvoiceNum(string invoiceNum)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                List<string> items = new List<string>(); // The list of items to be returned
                DataSet ds1 = _data.ExecuteSQLStatement(clsMainSQL.GetItemsByInvoiceNum(invoiceNum), ref i); // The DataSet of all the items
                foreach (DataRow row1 in ds1.Tables[0].Rows) // Each row (each individual item) in the ItemDesc table associated with an invoice num
                    items.Add(row1[0].ToString());
                return items;
            }
            catch
            {
                throw new Exception("Could not get items for invoice num " + invoiceNum);
            }
        }

        /// <summary>
        /// Adds an invoice to the data base
        /// </summary>
        /// <param name="invoiceDate">The date to be added with the new invoice</param>
        /// <param name="invoiceItems">The list of items to be added and associated with the new invoice</param>
        public static void AddInvoice(DateTime invoiceDate, List<string> invoiceItems)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                List<string> itemCodes = new List<string>(); // The list of item codes to be inserted and associated with the invoice
                decimal totalCost = 0; // Temp cost to be calculated and inserted

                foreach (var item in invoiceItems) // Each row (each individual item) in the list 
                {
                    DataSet ds = _data.ExecuteSQLStatement(clsMainSQL.GetItemDescRowByItemDesc(item), ref i); // The DataSet that holds the row item 
                    itemCodes.Add(ds.Tables[0].Rows[0][0].ToString());
                    totalCost += Convert.ToDecimal(ds.Tables[0].Rows[0][2]);
                }
                _data.ExecuteNonQuery(clsMainSQL.InsertInvoice(invoiceDate, totalCost)); // Insert the new invoice

                string newInvoiceNum = _data.ExecuteScalarSQL(clsMainSQL.GetNewestInvoiceNum); // The new invoice num inserted

                int lineNum = 0; // The temp line num for the line item
                foreach (var item in itemCodes) // Each line item associated with the new invoice
                    _data.ExecuteScalarSQL(clsMainSQL.InsertLineItems(newInvoiceNum, ++lineNum, item));
            }
            catch
            {
                throw new Exception("Could not add new invoice");
            }
        }

        /// <summary>
        /// Updates an invoice by invoice num with the parameters passed in
        /// </summary>
        /// <param name="invoiceNum">The invoice num of the invoice to be updated</param>
        /// <param name="invoiceDate">The date of the invoice to be updated to</param>
        /// <param name="invoiceItemNames">The items associated with the invoice to be updated, also used to calculate the cost</param>
        public static void UpdateInvoice(string invoiceNum, DateTime invoiceDate, List<string> invoiceItemNames)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                List<string> itemCodes = new List<string>(); // The list of item codes to be inserted and associated with the invoice
                decimal totalCost = 0; // Temp cost to be calculated and inserted

                foreach (var itemName in invoiceItemNames) // Each row (each individual item) in the list
                {
                    DataSet ds = _data.ExecuteSQLStatement(clsMainSQL.GetItemDescRowByItemDesc(itemName), ref i); // The DataSet that holds the row item
                    itemCodes.Add(ds.Tables[0].Rows[0][0].ToString());
                    totalCost += Convert.ToDecimal(ds.Tables[0].Rows[0][2]);
                }
                _data.ExecuteNonQuery(clsMainSQL.UpdateInvoice(invoiceNum, invoiceDate, totalCost)); // Update the invoice in the invoice table
                _data.ExecuteScalarSQL(clsMainSQL.DeleteLineItemsByInvoiceNum(invoiceNum)); // Deletes the existing line items 

                int lineNum = 0; // The temp line num for the line item
                foreach (var item in itemCodes) // Each line item associated with the new invoice
                    _data.ExecuteScalarSQL(clsMainSQL.InsertLineItems(invoiceNum, ++lineNum, item));
            }
            catch
            {
                throw new Exception("Could not update invoice with num " + invoiceNum);
            }
        }

        /// <summary>
        /// Deletes an invoice by the invoice num
        /// </summary>
        /// <param name="invoiceNum">The invoice num to delete</param>
        public static void DeleteInvoice(string invoiceNum)
        {
            try
            {
                _data.ExecuteScalarSQL(clsMainSQL.DeleteLineItemsByInvoiceNum(invoiceNum));
                _data.ExecuteScalarSQL(clsMainSQL.DeleteInvoiceByInvoiceNum(invoiceNum));
            }
            catch
            {
                throw new Exception("Could not delete invoice " + invoiceNum);
            }
        }

        /// <summary>
        /// Gets the cost on an individual item
        /// </summary>
        /// <param name="itemName">The name of the item</param>
        /// <returns>The cost of the item</returns>
        public static decimal GetCostOfItemByItemName(string itemName)
        {
            try
            {
                string cost = _data.ExecuteScalarSQL(clsMainSQL.GetCostOfItemByItemName(itemName)); // The cost of an item, as a string
                return decimal.Parse(cost, System.Globalization.NumberStyles.Currency);
            }
            catch
            {
                throw new Exception("Could not get cost of item for item " + itemName);
            }
        }

        /// <summary>
        /// This will return a list of all of the invoices the user wants based on the invoiceNumber
        /// </summary>
        /// <param name="invoiceNum">This is the invocie number the user wants</param>
        /// <returns></returns>
        public static List<Invoice> GetAllInvoicesByInvoice(string invoiceNum)
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsSearchSQL.GetAllInvoicesByInvoice(invoiceNum), ref i); // The DataSet of all the invoices
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
            catch
            {
                throw new Exception("Could not get all invoices for invoice " + invoiceNum);
            }
        }

        #endregion
    }

    #region Class

    /// <summary>
    /// The invoice class, used to hold an invoice object from the database 
    /// This is used both in this file and in the wndMain.xaml.cs file
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// The invoice num
        /// </summary>
        public string InvoiceNum { get; set; }
        /// <summary>
        /// The date of the invoice
        /// </summary>
        public string InvoiceDate { get; set; }
        /// <summary>
        /// The total cost of the invoice
        /// </summary>
        public string TotalCost { get; set; }
        /// <summary>
        /// All of the items associated with an invoice, comma separated
        /// </summary>
        public string Items { get; set; }
    }

    #endregion

}