using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CS3280_FinalProject
{

    class clsItemsLogic
    {
        #region Fields
        /// <summary>
        ///  connection to the database
        /// </summary>
        private static readonly clsDataAccess _data = new clsDataAccess();
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets all the items from the ItemsDesc table
        /// </summary>
        /// <returns>A list of all ItemCode, ItemDesc, and Cost </returns>
        public static List<Item> GetAllItems()
        {
            try
            {
                int i = 0; // Temp ref integer, unused but required
                DataSet ds = _data.ExecuteSQLStatement(clsItemsSQL.GetAllItems, ref i); // The DataSet of all the items 
                List<Item> items = new List<Item>(); // The list of items to be returned
                foreach (DataRow row in ds.Tables[0].Rows) // Each row (each individual item) in the ItemDesc table
                {
                    var lineItem = new Item()
                    {
                        ItemCode = row[0].ToString(),
                        ItemDesc = row[1].ToString(),
                        Cost = row[2].ToString(),
                    };
                    items.Add(lineItem);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception("Could not retrieve all items");
            }
        }

        /// <summary>
        /// Deletes an items by the item code
        /// </summary>
        /// <param name="itemCode">This is the item code the user wants to delete</param>
        public static void DeleteByItemCode(string itemCode)
        {
            try
            {
                int i = 0;
                DataSet ds = _data.ExecuteSQLStatement(clsItemsSQL.GetLineItemsByItemCode(itemCode), ref i);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    var codes = "";
                    foreach (DataRow row in ds.Tables[0].Rows)
                        codes += row.ItemArray[0] + ", ";

                    codes = codes.Trim(new char[] { ' ', ',' });
                    MessageBox.Show("Cannot delete an item that is in an invoice! Item in invoice(s): " + codes);
                    return;
                }
                _data.ExecuteScalarSQL(clsItemsSQL.DeleteItemsByItemCode(itemCode));
            }
            catch
            {
                throw new Exception("Could not delete item " + itemCode);
            }
        }

        /// <summary>
        /// Insert a new item to database table ItemDesc
        /// </summary>
        /// <param name="itemCode">Anew item code to be inserted to table </param>
        /// <param name="itemDesc">Anew item desc to be inserted to table</param>
        /// <param name="itemCost">Anew item cost to be inserted to table</param>
        public static void AddItem(string itemCode, string itemDesc, string itemCost)
        {
            try
            {
                _data.ExecuteNonQuery(clsItemsSQL.InsertItem(itemCode, itemDesc, itemCost)); // Insert the new item
            }
            catch (Exception e)
            {
                throw new Exception("Could not add item " + itemDesc);
            }
        }

        /// <summary>
        /// Updates an existing item in the database
        /// </summary>
        /// <param name="item">A item to be updated</param>
        public static void UpdateItem(Item item)
        {
            try
            {
                _data.ExecuteNonQuery(clsItemsSQL.UpdateItem(item));
            }
            catch (Exception e)
            {
                throw new Exception("Could not edit item " + item.ItemCode);
            }
        }

        #endregion

    }

    /// <summary>
    /// //////////////////////////////////////////////////
    /// </summary>
    public class Item
    {
        #region properties
        /// <summary>
        /// the properity of the class holds ItemCode 
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// the properity of the class holds item description  
        /// </summary>
        public string ItemDesc { get; set; }

        /// <summary>
        /// the properity of the class holds item Cost
        /// </summary>
        public string Cost { get; set; }
        #endregion

    }
}