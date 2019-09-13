using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_FinalProject
{
    /// <summary>
    /// Static class which holds all the SQL statements for the Main Window and its logic
    /// </summary>
    public static class clsMainSQL
    {
        /// <summary>
        /// Statement to retrieve all of the invoices from the Invoices table
        /// </summary>
        public static string GetAllInvoices = "SELECT * " +
                                              "FROM Invoices";

        /// <summary>
        /// This statement will grab all of the invoice numbers
        /// </summary>
        public static string getAllInvoiceNumber = "SELECT InvoiceNum " +
                                                   "FROM Invoices ORDER BY";

        /// <summary>
        /// This statement will grab all of the totalcost of all of the invoices
        /// </summary>
        public static string getAllInvoiceTotal = "SELECT TotalCost " +
                                                  "FROM Invoices ORDER BY TotalCost";

        /// <summary>
        /// Statement to retrive all of the item names from the ItemDesc table
        /// </summary>
        public static string GetAllItems = "SELECT ItemDesc " +
                                           "FROM ItemDesc";

        /// <summary>
        /// Statement to retrieve the most latest invoice to be created
        /// </summary>
        public static string GetNewestInvoiceNum = "SELECT TOP 1 InvoiceNum " +
                                                   "FROM Invoices " +
                                                   "ORDER BY InvoiceNum DESC";

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
        /// Statement to retrieve the line item's info by the name of the item
        /// </summary>
        /// <param name="itemDesc">The name of the item</param>
        /// <returns>The completed statement</returns>
        public static string GetItemDescRowByItemDesc(string itemDesc) =>
            "SELECT * " +
            "FROM ItemDesc " +
            "WHERE ItemDesc = '" + itemDesc + "'";

        /// <summary>
        /// Statement to insert a new invoice
        /// </summary>
        /// <param name="date">The date associated with an invoice</param>
        /// <param name="cost">The cost associated with an invoice</param>
        /// <returns>The completed statement</returns>
        public static string InsertInvoice(DateTime date, decimal cost) =>
            "INSERT INTO Invoices (InvoiceDate, TotalCost) Values ('" + date.ToString("MM/dd/yyyy") + "', " + cost + ")";

        /// <summary>
        /// Statement to update an existing invoice
        /// </summary>
        /// <param name="invoiceNum">The invoice num to update</param>
        /// <param name="date">The date to update the invoice to</param>
        /// <param name="cost">The cost to update the invoice to</param>
        /// <returns>The completed statement</returns>
        public static string UpdateInvoice(string invoiceNum, DateTime date, decimal cost) =>
            "UPDATE Invoices SET InvoiceDate = '" + date.ToString("MM/dd/yyyy") + "', TotalCost = " + cost + " WHERE InvoiceNum = " + invoiceNum;

        /// <summary>
        /// Statement to insert new line items
        /// </summary>
        /// <param name="invoiceNum">The invoice num associated with the line item</param>
        /// <param name="lineItemNum">The line item num to insert</param>
        /// <param name="itemCode">The item code to be inserted</param>
        /// <returns>The completed statement</returns>
        public static string InsertLineItems(string invoiceNum, int lineItemNum, string itemCode) =>
            "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (" + invoiceNum + ", " + lineItemNum + ", '" + itemCode + "')";

        /// <summary>
        /// Statement to delete all line items associated with an invoice
        /// </summary>
        /// <param name="invoiceNum">The invoice num associated with the line items</param>
        /// <returns>The completed statement</returns>
        public static string DeleteLineItemsByInvoiceNum(string invoiceNum) =>
            "DELETE FROM LineItems WHERE InvoiceNum = " + invoiceNum;

        /// <summary>
        /// Statement to delete an individual invoice by the invoice num (index)
        /// </summary>
        /// <param name="invoiceNum">The invoice num associated with the invoice</param>
        /// <returns>The completed statement</returns>
        public static string DeleteInvoiceByInvoiceNum(string invoiceNum) =>
            "DELETE FROM Invoices WHERE InvoiceNum = " + invoiceNum;

        /// <summary>
        /// Statement to retrieve cost of an individual item
        /// </summary>
        /// <param name="itemName">The name of an item</param>
        /// <returns>The completed statement</returns>
        public static string GetCostOfItemByItemName(string itemName) =>
            "SELECT Cost FROM ItemDesc WHERE ItemDesc = '" + itemName + "'";

        /// <summary>
        /// This will grab all of the invoices that match the value that user selected
        /// </summary>
        /// <param name="invoiceNum">This is the invoice that they are looking for</param>
        /// <returns></returns>
        public static string GetAllInvoicesByInvoice(string invoiceNum) =>
                                "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum + " ORDER BY InvoiceNum";
    }
}