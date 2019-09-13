using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280_FinalProject
{
    /// <summary>
    /// Static class which holds all the SQL statements for the Items Window and its logic
    /// </summary>
    class clsItemsSQL
    {

        ///// <summary>
        ///// Statement to retrieve all of the items from the ItemDesc table.
        ///// </summary>
        //public static string GetAllItems = "SELECT ItemCode, ItemDesc, Cost" +
        //                                   "FROM ItemDesc";

        /// <summary>
        /// Statement to retrieve all of the items from the ItemDesc table.
        /// </summary>
        public static string GetAllItems = "SELECT * " +
                                           "FROM ItemDesc";



        /// <summary>
        /// Statement to retrieve the distinct InvoiceNum from LineItems table.
        /// </summary>
        /// <param name="itemCode"> for selected item code</param>
        /// <returns>The completed statement</returns>
        public static string GetDistinctItems(string itemCode) =>
            "SELECT distinct InvoiceNum " +
            "FROM LineItems " +
            "WHERE ItemCode = " + itemCode;

        /// <summary>
        /// Statement to update itemDesc 
        /// </summary>
        /// <param name="itemDesc"> the item descreption to update new descreption</param>
        /// <param name="icost"> the cost to be updated </param>
        /// <param name="itemCode"> the itemcode want to be updated </param>
        /// <returns>The completed statement</returns>
        public static string UpdateItem(Item item) =>
            "UPDATE ItemDesc SET ItemDesc = '" + item.ItemDesc + "', Cost = " + item.Cost + " WHERE ItemCode = '" + item.ItemCode +"'";


        /// <summary>
        /// Statement to insert a new item to itemDesc table
        /// </summary>
        /// <param name="itemCod">A new item code to be inserted</param>
        /// <param name="itemDesc">A new Item Descreption to be inserted</param>
        /// <param name="cost">The cost for the new item to be inserted</param>
        /// <returns>The completed statement</returns>
        public static string InsertItem(string itemCode, string itemDesc, string cost) =>
            "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) Values ('" + itemCode + "', '" + itemDesc + "', " + cost + ")";

        /// <summary>
        /// Statement to delete all ItemDesc associated with line item code
        /// </summary>
        /// <param name="itemCode">The item code associated with the line items</param>
        /// <returns>The completed statement</returns>
        public static string DeleteItemsByItemCode(string itemCode) =>
            "DELETE FROM ItemDesc WHERE ItemCode = '" + itemCode + "'";

        /// <summary>
        /// Statement to get all the line items by the item code
        /// </summary>
        /// <param name="itemCode">A item code to query on</param>
        /// <returns></returns>
        public static string GetLineItemsByItemCode(string itemCode) =>
            "SELECT * FROM LineItems WHERE ItemCode = '" + itemCode + "'";

    }
}
