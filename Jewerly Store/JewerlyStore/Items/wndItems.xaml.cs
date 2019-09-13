using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CS3280_FinalProject
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : MetroWindow
    {
        /// <summary>
        /// Default Constructor for the Items window
        /// </summary>
        public wndItems()
        {
            InitializeComponent();
            grItemDesc.ItemsSource = clsItemsLogic.GetAllItems();
        }

        /// <summary>
        /// Executes when the "Edit" button is clicked
        /// Populates the Add/Edit "form" with the row's information
        /// </summary>
        /// <param name="sender">The button and row that was selected</param>
        /// <param name="e">Unused</param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = (Button)sender; // The button/row selected
                var Item = (Item)button.DataContext; // The Data Context (Item) that was selected
                ItemNumLbl.Content = "Item Code: " + Item.ItemCode;
                TotalCostTxtBox.Text = Item.Cost;
                ItemDescTxtBox.Text = Item.ItemDesc;
                ItemAddEditLbl.Content = "Edit Item";
                AddItem.Content = "Save Item";
                AddItemSp.Visibility = Visibility.Visible;
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Executes when the "Delete" button is clicked
        /// Deletes the Item and its associated line items
        /// </summary>
        /// <param name="sender">The button and row that was selected</param>
        /// <param name="e">Unused</param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = (Button)sender; // The button/row selected
                var Item = (Item)button.DataContext; // The Data context (Item) that was selected
                AddItemSp.Visibility = Visibility.Hidden;
                clsItemsLogic.DeleteByItemCode(Item.ItemCode);
                grItemDesc.ItemsSource = clsItemsLogic.GetAllItems();
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Executes when the New Item button is clicked
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void NewItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemAddEditLbl.Content = "New Item";
                AddItem.Content = "Add Item";
                AddItemSp.Visibility = Visibility.Visible;
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Executes when the Add Item/Save Item button is clicked
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ItemAddEditLbl.Content.ToString() == "New Item")
                {
                    List<string> allInvoiceCodes = clsItemsLogic.GetAllItems().Select(a => a.ItemCode).ToList();
                    StringBuilder itemCode = new StringBuilder("A");
                    while (allInvoiceCodes.Contains(itemCode.ToString()))
                    {
                        int i = itemCode.Length - 1;
                        while (i >= 0 && itemCode[i] == 'Z')
                        {
                            itemCode[i] = 'A';
                            i--;
                        }
                        if (i == -1)
                            itemCode.Insert(0, 'A');
                        else
                            itemCode[i]++;
                    }

                    clsItemsLogic.AddItem(itemCode.ToString(), ItemDescTxtBox.Text, TotalCostTxtBox.Text);
                }
                else
                {
                    var newItem = new Item()
                    {
                        ItemCode = ItemNumLbl.Content.ToString().Split(':')[1].Trim(),
                        Cost = TotalCostTxtBox.Text,
                        ItemDesc = ItemDescTxtBox.Text,
                    };
                    clsItemsLogic.UpdateItem(newItem);
                }
                AddItemSp.Visibility = Visibility.Hidden;
                grItemDesc.ItemsSource = clsItemsLogic.GetAllItems();
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Shows a message box with the errors thrown
        /// </summary>
        /// <param name="sClass">This is the class it happened in</param>
        /// <param name="sMethod">This is the method</param>
        /// <param name="sMessage">This is the error message</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}