using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using MahApps.Metro.Controls;

namespace CS3280_FinalProject
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : MetroWindow
    {
        #region Fields and Properties 

        /// <summary>
        /// The list to hold all of the items and if they were checked. Refreshes per invoice.
        /// </summary>
        public ObservableCollection<BoolStringClass> ItemsCheckBoxList { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for the main window.
        /// Sets the main data grid with all the current invoices and populates the List Box of items (unchecked) for adding/editting invoices.
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();
                InvoiceDataGrid.ItemsSource = clsMainLogic.GetAllInvoices();
                var items = clsMainLogic.GetAllItems(); // All the possible items
                ItemsCheckBoxList = new ObservableCollection<BoolStringClass>();
                foreach (var item in items) // Each item in the possible items, adds it to a class object which is then added to a list
                    ItemsCheckBoxList.Add(new BoolStringClass { IsSelected = false, ItemName = item });
                ListBox1.ItemsSource = ItemsCheckBoxList;
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Executes when the "New Invoice" button is clicked
        /// Sets the visibility and content of the Add/Edit "form"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal totalCost = 0; // The default cost
                InvoiceAddEditLbl.Content = "Add Invoice";
                InvoiceNumLbl.Content = "Invoice Num: TBD";
                ListBox1.ItemsSource = null;
                foreach (var item in ItemsCheckBoxList) // Each item in the list, ensures the check box is not checked
                    item.IsSelected = false;
                ListBox1.ItemsSource = ItemsCheckBoxList;
                NewDate.SelectedDate = null;
                TotalCostLbl.Content = "Total Cost: " + totalCost.ToString("c");
                AddInvoice.Content = "Add Invoice";
                AddInvoiceSp.Visibility = Visibility.Visible;
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Executes when the "Add/Save Invoice" button is clicked
        /// If it's an edit, it updates the invoice and re-adds the line items
        /// If it's an add, it creates and adds the invoice and line items
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddInvoiceSp.Visibility = Visibility.Hidden;
                InvoiceDataGrid.ItemsSource = null;
                if (InvoiceAddEditLbl.Content.ToString() == "Edit Invoice")
                {
                    var invoiceNumLbl = InvoiceNumLbl.Content.ToString().Split(' ');
                    var invoiceNum = invoiceNumLbl[2];
                    clsMainLogic.UpdateInvoice(invoiceNum, NewDate.SelectedDate ?? DateTime.Now, ItemsCheckBoxList.Where(a => a.IsSelected).Select(a => a.ItemName).ToList());
                }
                else
                    clsMainLogic.AddInvoice(NewDate.SelectedDate ?? DateTime.Now, ItemsCheckBoxList.Where(a => a.IsSelected).Select(a => a.ItemName).ToList());
                InvoiceDataGrid.ItemsSource = clsMainLogic.GetAllInvoices();
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
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
                var invoice = (Invoice)button.DataContext; // The Data Context (invoice) that was selected
                InvoiceNumLbl.Content = "Invoice Num: " + invoice.InvoiceNum;
                NewDate.SelectedDate = Convert.ToDateTime(invoice.InvoiceDate);
                var items = clsMainLogic.GetItemsByInvoiceNum(invoice.InvoiceNum); // All of the items associated with the invoice
                ListBox1.ItemsSource = null;
                foreach (var item in ItemsCheckBoxList) // Each item in the full check box list. Sets selected if it was found in the database
                    item.IsSelected = items.Contains(item.ItemName);
                ListBox1.ItemsSource = ItemsCheckBoxList;
                TotalCostLbl.Content = "Total Cost: " + invoice.TotalCost;
                InvoiceAddEditLbl.Content = "Edit Invoice";
                AddInvoice.Content = "Save Invoice";
                AddInvoiceSp.Visibility = Visibility.Visible;
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Executes when the "Delete" button is clicked
        /// Deletes the invoice and its associated line items
        /// </summary>
        /// <param name="sender">The button and row that was selected</param>
        /// <param name="e">Unused</param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = (Button)sender; // The button/row selected
                var invoice = (Invoice)button.DataContext; // The Data context (invoice) that was selected
                AddInvoiceSp.Visibility = Visibility.Hidden;
                InvoiceDataGrid.ItemsSource = null;
                clsMainLogic.DeleteInvoice(invoice.InvoiceNum);
                InvoiceDataGrid.ItemsSource = clsMainLogic.GetAllInvoices();
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Executes whenever any check box in the list of items is selected
        /// Calculates the total cost
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal totalCost = 0; // Hold the calculated total cost
                foreach (var item in ItemsCheckBoxList.Where(a => a.IsSelected)) // Each selected item in the check box list
                    totalCost += clsMainLogic.GetCostOfItemByItemName(item.ItemName);
                TotalCostLbl.Content = "Total Cost: " + totalCost.ToString("c");
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Opens the Update Def Table Window (Items Window)
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void UpdateDefTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new wndItems();
                window.ShowDialog();
                InvoiceDataGrid.ItemsSource = clsMainLogic.GetAllInvoices();
                var items = clsMainLogic.GetAllItems(); // All the possible items
                ItemsCheckBoxList = new ObservableCollection<BoolStringClass>();
                foreach (var item in items) // Each item in the possible items, adds it to a class object which is then added to a list
                    ItemsCheckBoxList.Add(new BoolStringClass { IsSelected = false, ItemName = item });
                ListBox1.ItemsSource = ItemsCheckBoxList;
            }
            catch (Exception ex) // The exception to be thrown
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Opens the Search Window and handles the return by opening the edit for the returned item.
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="e">Unused</param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new wndSearch();
                var invoiceNum = window.ShowWindow();
                if (string.IsNullOrEmpty(invoiceNum))
                    return;
                var invoice = clsMainLogic.GetAllInvoicesByInvoice(invoiceNum).First();
                InvoiceNumLbl.Content = "Invoice Num: " + invoice.InvoiceNum;
                NewDate.SelectedDate = Convert.ToDateTime(invoice.InvoiceDate);
                var items = clsMainLogic.GetItemsByInvoiceNum(invoice.InvoiceNum); // All of the items associated with the invoice
                ListBox1.ItemsSource = null;
                foreach (var item in ItemsCheckBoxList) // Each item in the full check box list. Sets selected if it was found in the database
                    item.IsSelected = items.Contains(item.ItemName);
                ListBox1.ItemsSource = ItemsCheckBoxList;
                TotalCostLbl.Content = "Total Cost: " + invoice.TotalCost;
                InvoiceAddEditLbl.Content = "Edit Invoice";
                AddInvoice.Content = "Save Invoice";
                AddInvoiceSp.Visibility = Visibility.Visible;
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
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
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

        #endregion

        #region Classes

        /// <summary>
        /// The class to hold the check box item information
        /// </summary>
        public class BoolStringClass
        {
            /// <summary>
            /// The name of the item
            /// </summary>
            public string ItemName { get; set; }
            /// <summary>
            /// Whether the item is selected or not
            /// </summary>
            public bool IsSelected { get; set; }
        }

        #endregion
    }
}
