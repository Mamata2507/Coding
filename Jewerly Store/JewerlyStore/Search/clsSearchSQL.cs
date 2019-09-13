using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_FinalProject
{
    class clsSearchSQL
    {
        /// <summary>
        /// Statement to retrieve all of the invoices from the Invoices table
        /// </summary>
        public static string GetAllInvoices = "SELECT DISTINCT InvoiceNum " +
                                              "FROM Invoices ORDER BY InvoiceNum ASC";

        /// <summary>
        /// This will grab all invoices no matter what
        /// </summary>
        public static string GetAllInvoicesData = "SELECT * " +
                                      "FROM Invoices";

        /// <summary>
        /// This will grab all of the unique values for the invoice table by invoicedate
        /// </summary>
        public static string getAllDates = "SELECT DISTINCT InvoiceDate " +
                                      "FROM Invoices ORDER BY InvoiceDate ASC";

        /// <summary>
        /// This will graba all of the unique Totals 
        /// </summary>
        public static string getAllTotals = "SELECT DISTINCT TotalCost " +
                                              "FROM Invoices ORDER BY TotalCost ASC";

        /// <summary>
        /// This will grab all of the invoice numbers that are in the table
        /// </summary>
        public static string getAllInvoiceNums = "SELECT DISTINCT InvoiceNum " +
                                                "FROM Invoices ORDER BY InvoiceNum ASC";

        /// <summary>
        /// Statement to retrieve all the line item names associated with an invoice num
        /// </summary>
        /// <param name="invoiceNum">The invoice num</param>
        /// <returns>The completed statement</returns>
        public static string GetItemsByInvoiceNum(string invoiceNum) =>
            "SELECT ItemDesc " +
            "FROM ItemDesc INNER JOIN LineItems ON ItemDesc.ItemCode = LineItems.ItemCode " +
            "WHERE InvoiceNum = " + invoiceNum;

        /// <summary>
        /// This will grab all of the invoices that match the value that user selected
        /// </summary>
        /// <param name="invoiceNum">This is the invoice that they are looking for</param>
        /// <returns></returns>
        public static string GetAllInvoicesByInvoice(string invoiceNum) =>
                                "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum + " ORDER BY InvoiceNum";

        /// <summary>
        /// This will grab all of the invoices where the invoicedate is matched
        /// </summary>
        /// <param name="invoiceDate">This is the date that the user selected</param>
        /// <returns></returns>
        public static string GetAllInvoicesByDate(DateTime invoiceDate) =>
                                "SELECT * FROM Invoices WHERE InvoiceDate = #" +invoiceDate + "# ORDER BY InvoiceNum";


        /// <summary>
        /// This will grab all of the information regarding the invoice if the total cost the user picked matches
        /// </summary>
        /// <param name="totalCost">This is the totalcost the user is looking for</param>
        /// <returns></returns>
        public static string GetAllInvoicesByTotal(string totalCost) =>
                                "SELECT * " + "FROM Invoices WHERE TotalCost = " + totalCost + " ORDER BY InvoiceNum";

        /// <summary>
        /// This is going to seach on all 3 cretia
        /// </summary>
        /// <param name="inv">This is the invoice the user wants</param>
        /// <param name="date">This is the date the user wants</param>
        /// <param name="total">This is the total the user wants</param>
        /// <returns></returns>
        public static string GetAllInvoicesByEverything(string inv, string date, string total) =>
                                "SELECT * " + "FROM Invoices WHERE InvoiceNum = " + inv + " AND InvoiceDate = #" + date + "# AND TotalCost = " + total + " ORDER BY InvoiceNum ASC";

        /// <summary>
        /// This is a SQL statement for total and date#
        /// </summary>
        /// <param name="total">The total the user is looking for</param>
        /// <param name="date">The date the user is looking for</param>
        /// <returns></returns>
        public static string GetAllInvoicesByTotalAndDate(string total, string date) =>
                                "SELECT * FROM Invoices WHERE Totalcost = " + total + " AND InvoiceDate = #" + date + "# ORDER BY InvoiceNum";

        /// <summary>
        /// This is the SQL state for total and invoice
        /// </summary>
        /// <param name="total">The total the user is looking for</param>
        /// <param name="invoice">The invoice the user is looking for</param>
        /// <returns></returns>
        public static string GetAllInvoicesByTotalAndInvoice(string total, string invoice) =>
                                "SELECT * FROM Invoices WHERE TotalCost = " + total + " AND InvoiceNum = " + invoice + " ORDER BY InvoiceNum";

        /// <summary>
        /// This a sql statement for invoice and date
        /// </summary>
        /// <param name="invoice">This is the invoice the user is looking for</param>
        /// <param name="date">This is the date the user is looking for.</param>
        /// <returns></returns>
        public static string GetAllInvoicesByInvoiceAndDate(string invoice, string date) =>
                                "SELECT * FROM Invoices WHERE InvoiceNum = " + invoice + " AND InvoiceDate = #" + date + "# ORDER BY InvoiceNum";         
    }
}
