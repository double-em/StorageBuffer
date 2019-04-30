using System;
using System.Collections.Generic;
using System.Linq;
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
using StorageBuffer.Domain;

namespace StorageBuffer
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private Order order;
        public OrderWindow(Order order)
        {
            InitializeComponent();

            this.order = order;
            Setup();
            this.Show();
        }

        private void Setup()
        {
            lOrderNumber.Content = "Ordrenummer: " + order.Id;
            lOrderDate.Content = "Ordredato: " + order.Date;
            lDeadline.Content = "Deadline: " + order.Deadline;
            lCustomerName.Content = "Kunde: " + order.CustomerObj.Name;

            switch (order.OrderStatus)
            {
                case Status.Received:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[0];
                    break;

                case Status.InProgress:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[1];
                    break;

                case Status.Done:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[2];
                    break;

                case Status.Shipped:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[3];
                    break;

                case Status.Billed:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[4];
                    break;

                case Status.Paid:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[5];
                    break;
                case Status.Canceled:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[6];
                    break;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
