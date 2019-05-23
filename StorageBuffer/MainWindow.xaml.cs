using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBuffer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public delegate void NotifyItemChanged();
    public partial class MainWindow : Window
    {
        private Controller control;
        private List<OrderWindow> orderWindows;
        private List<CustomerWindow> customerWindows;
        private List<MaterialWindow> materialWindows;

        
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                control = Controller.Instance;

                try
                {
                    control.GetAllData(DatabaseRepo.Instance);
                }
                catch (Exception e)
                {
                    throw new Exception($"Kunne ikke oprette forbindelse til Databasen.\nTjek at computeren har adgang til internettet.\n\nTeknisk info:\n{e.Message}");
                }

                orderWindows = new List<OrderWindow>();
                customerWindows = new List<CustomerWindow>();
                materialWindows = new List<MaterialWindow>();
                GetAllItems();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Programmet kunne ikke starte grundet følgende fejl:\n\n{e.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void GetAllItems()
        {
            foreach (List<string> item in control.FindItems("All"))
            {
                lvResult.Items.Add( new {Type = item[0], Id = item[1], Name = item[2], Data = item[3] });
            }
        }

        private void tbInactive_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Foreground != Brushes.Black)
            {
                box.Clear();
                box.Foreground = Brushes.Black;
            }
        }

        private void TbSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void CbChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            if (control != null)
            {
                string searchQuery = tbSearchBar.Text;

                if (tbSearchBar.Foreground != Brushes.Black)
                {
                    searchQuery = "";
                }

                lvResult.Items.Clear();
                string criteria = "";
                switch (cbChoice.SelectedIndex)
                {
                    case 0:
                        criteria = "All";
                        break;

                    case 1:
                        criteria = "Materials";
                        break;

                    case 2:
                        criteria = "Orders";
                        break;

                    case 3:
                        criteria = "Customers";
                        break;
                }

                foreach (List<string> item in control.FindItems(criteria, searchQuery))
                {
                    lvResult.Items.Add(new { Type = item[0], Id = item[1], Name = item[2], Data = item[3] });
                }
            }
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = (ListViewItem) sender;
            var item = listViewItem.Content;

            PropertyInfo[] props = item.GetType().GetProperties();

            switch (props[0].GetValue(item, null))
            {
                case "Customer":
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        CustomerWindow customerWindow = new CustomerWindow(control, int.Parse(props[1].GetValue(item, null).ToString()));
                        customerWindows.Add(customerWindow);
                        customerWindow.Top = Top;
                        customerWindow.Left = Left;
                        customerWindow.AddObserver(GetItemChanged);
                        customerWindow.Show();
                    }));
                    break;

                case "Material":
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        MaterialWindow materialWindow = new MaterialWindow(control, int.Parse(props[1].GetValue(item, null).ToString()));
                        materialWindows.Add(materialWindow);
                        materialWindow.Top = Top;
                        materialWindow.Left = Left;
                        materialWindow.AddObserver(GetItemChanged);
                        materialWindow.Show();
                    }));
                    break;

                case "Order":
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        OrderWindow orderWindow = new OrderWindow(control, int.Parse(props[1].GetValue(item, null).ToString()));
                        orderWindows.Add(orderWindow);
                        orderWindow.Top = Top;
                        orderWindow.Left = Left;
                        orderWindow.AddObserver(GetItemChanged);
                        orderWindow.Show();
                    }));
                    break;
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (orderWindows != null && customerWindows != null && materialWindows != null)
            {
                foreach (OrderWindow window in orderWindows)
                {
                    window.Close();
                }

                foreach (CustomerWindow window in customerWindows)
                {
                    window.Close();
                }

                foreach (MaterialWindow window in materialWindows)
                {
                    window.Close();
                }
            }
        }

        private int createOrderCustomerId;

        private void BtnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            string messsage = "";
            if (control.CreateOrder(createOrderCustomerId, tbOrderName.Text, tbOrderDeadline.Text, tbOrderComment.Text))
            {
                messsage = "Ordren blev oprettet.";

                createOrderCustomerId = 0;
                tbOrderName.Text = "";
                lCustomerName.Content = "Ingen Kunde Valgt";
                tbOrderDeadline.Text = "";
                tbOrderComment.Text = "";
            }
            else
            {
                messsage = "Ordren kunne IKKE oprettes.";
            }

            MessageWindow messageWindow = new MessageWindow(messsage);
            messageWindow.Owner = this;
            messageWindow.Top = this.Top;
            messageWindow.Left = this.Left + 8;
            messageWindow.ShowDialog();
        }

        private void BtnChooseCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerChooseWindow customerChooseWindow = new CustomerChooseWindow(control);
            customerChooseWindow.Owner = this;
            customerChooseWindow.Top = Top;
            customerChooseWindow.Left = Left + 8;
            customerChooseWindow.ShowDialog();

            if (customerChooseWindow.CustomerId == 0)
            {
                lCustomerName.Content = "Ingen Kunde Valgt";
            }
            else
            {
                createOrderCustomerId = customerChooseWindow.CustomerId;
                lCustomerName.Content = $"{customerChooseWindow.CustomerName} (Kundenummer: {createOrderCustomerId})";
            }
        }

        private void BtnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            string messsage = "";
            if (control.CreateCustomer(
                tbCreateCustomerName.Text,
                tbCreateCustomerAddress.Text,
                tbCreateCustomerCity.Text,
                tbCreateCustomerZip.Text,
                tbCreateCustomerPhone.Text,
                tbCreateCustomerEmail.Text,
                tbCreateCustomerComment.Text))
            {
                messsage = "Kunden blev oprettet.";

                tbCreateCustomerName.Text = "";
                tbCreateCustomerAddress.Text = "";
                tbCreateCustomerCity.Text = "";
                tbCreateCustomerZip.Text = "";
                tbCreateCustomerPhone.Text = "";
                tbCreateCustomerEmail.Text = "";
                tbCreateCustomerComment.Text = "";
            }
            else
            {
                messsage = "Kunden kunne IKKE oprettes.";
            }

            MessageWindow messageWindow = new MessageWindow(messsage);
            messageWindow.Owner = this;
            messageWindow.Top = this.Top;
            messageWindow.Left = this.Left + 8;
            messageWindow.ShowDialog();
        }

        private void BtnCreateMaterial_Click(object sender, RoutedEventArgs e)
        {
            string messsage = "";
            if (control.CreateMaterial(tbCreateMaterialName.Text, tbCreateMaterialComments.Text, tbCreateMaterialQuantity.Text))
            {
                messsage = "Materialet blev oprettet.";

                tbCreateMaterialName.Text = "";
                tbCreateMaterialComments.Text = "";
                tbCreateMaterialQuantity.Text = "";
            }
            else
            {
                messsage = "Materialet kunne IKKE oprettes.";
            }

            MessageWindow messageWindow = new MessageWindow(messsage);
            messageWindow.Owner = this;
            messageWindow.Top = this.Top;
            messageWindow.Left = this.Left + 8;
            messageWindow.ShowDialog();
        }

        public void GetItemChanged()
        {
            Search();
        }
    }
}
