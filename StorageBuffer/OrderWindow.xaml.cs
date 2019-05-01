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
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBuffer
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private Controller control;
        private Order order;
        public OrderWindow(Controller control, Order order)
        {
            InitializeComponent();

            this.order = order;
            this.control = control;
            Setup();
            this.Show();
        }

        private void Setup()
        {
            Title = "Ordre - " + order.Id + " : " + order.Name;
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

        private void AddMaterial_Click(object sender, RoutedEventArgs e)
        {
            MaterialChooseWindow chooseWindow = new MaterialChooseWindow(control);

            chooseWindow.Owner = this;
            chooseWindow.Top = this.Top;
            chooseWindow.Left = this.Left + 8;
            chooseWindow.ShowDialog();

            bool materialAlreadyAdded = false;
            foreach (Orderline orderline in order.orderlines)
            {
                if (orderline.MaterialId == chooseWindow.ChoosenMaterial.Id)
                {
                    materialAlreadyAdded = true;
                    break;
                }
            }

            if (!materialAlreadyAdded)
            {
                Orderline orderlineNew = new Orderline(chooseWindow.ChoosenMaterial, 0, DateTime.Now.ToShortDateString());
                order.orderlines.Add(orderlineNew);
                lvResult.Items.Add(orderlineNew);
            }
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
