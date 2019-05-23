using System;
using System.Collections.Generic;
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
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBuffer
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private Controller control;
        private int customerId;

        private bool removeCustomer = false;

        public CustomerWindow(Controller control, int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
            this.control = control;
            Setup();
            this.Show();
        }

        private void Setup()
        {
            lCustomerName.Content = $"Navn (Kundenummmer {customerId})";
            GetCustomerInfo();
        }

        private void GetCustomerInfo()
        {
            List<string> customer = control.GetCustomer(customerId);

            tbCustomerName.Text = customer[1];
            tbCustomerAddress.Text = customer[2];
            tbCustomerCity.Text = customer[3];
            tbCustomerZip.Text = customer[4];
            tbCustomerPhone.Text = customer[5];
            tbCustomerEmail.Text = customer[6];
            tbCustomerComment.Text = customer[7];
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveCustomer();
        }

        void SaveCustomer()
        {
            string messsage = "";
            if (control.UpdateCustomer(
                customerId,
                tbCustomerName.Text,
                tbCustomerAddress.Text,
                tbCustomerCity.Text,
                tbCustomerZip.Text,
                tbCustomerPhone.Text,
                tbCustomerEmail.Text,
                tbCustomerComment.Text))
            {
                messsage = "Kunden blev gemt.";
            }
            else
            {
                messsage = "Kunden kunne IKKE gemmes.";
            }

            MessageWindow messageWindow = new MessageWindow(messsage);
            messageWindow.Owner = this;
            messageWindow.Top = this.Top;
            messageWindow.Left = this.Left + 8;
            messageWindow.ShowDialog();
        }

        private void CustomerWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (!removeCustomer)
            {
                ConfirmationWindow confirmationWindow = new ConfirmationWindow("Vil du gemme ændringerne?");
                confirmationWindow.Owner = this;
                confirmationWindow.Top = this.Top;
                confirmationWindow.Left = this.Left + 8;
                confirmationWindow.ShowDialog();

                if ((bool)confirmationWindow.DialogResult)
                {
                    SaveCustomer();
                }
            }
        }

        private void BtnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationWindow confirmationWindow = new ConfirmationWindow("Er du sikker på du vil SLETTE kunden?");
            confirmationWindow.Owner = this;
            confirmationWindow.Top = this.Top;
            confirmationWindow.Left = this.Left + 8;
            confirmationWindow.ShowDialog();

            if ((bool)confirmationWindow.DialogResult)
            {
                RemoveCustomer();
            }
        }

        private void RemoveCustomer()
        {
            string messsage = "";
            bool removed = false;
            if (control.RemoveCustomer(customerId))
            {
                messsage = "Kunden blev slettet.";
                removed = true;
            }
            else
            {
                messsage = "Kunden kunne IKKE slettes.";
            }

            MessageWindow messageWindow = new MessageWindow(messsage);
            messageWindow.Owner = this;
            messageWindow.Top = this.Top;
            messageWindow.Left = this.Left + 8;
            messageWindow.ShowDialog();

            if (removed)
            {
                removeCustomer = true;
                this.Close();
            }
        }
    }
}
