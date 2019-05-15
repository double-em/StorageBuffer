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
using StorageBuffer.Application;
using StorageBuffer.Domain;

public delegate void OrderChanged(Order order);

namespace StorageBuffer
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private Controller control;
        private int orderId;
        private string orderName;
        private string orderDate;
        private string orderDeadline;
        private string customerName;
        private string orderStatus;
        private List<List<string>> orderlines;

        public OrderWindow(Controller control, int orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
            this.control = control;
            Setup();
            this.Show();
        }

        private void Setup()
        {
            GetOrderInfo();
            GetAllOrderlines();
            orderlines = control.GetOrderlines(orderId);
        }

        private void GetOrderInfo()
        {
            List<string> orderInfo = control.GetOrderInfo(orderId);
            orderName = orderInfo[1];
            orderDate = orderInfo[2];
            orderDeadline = orderInfo[3];
            customerName = orderInfo[4];
            orderStatus = orderInfo[5];

            Title = "Ordre - " + orderId + " : " + orderName;
            lOrderNumber.Content = "Ordrenummer: " + orderId;
            lOrderDate.Content = "Ordredato: " + orderDate;
            lDeadline.Content = "Deadline: " + orderDeadline;
            lCustomerName.Content = "Kunde: " + customerName;

            switch (orderStatus)
            {
                case "Received":
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[0];
                    break;

                case "InProgress":
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[1];
                    break;

                case "Done":
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[2];
                    break;

                case "Shipped":
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[3];
                    break;

                case "Billed":
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[4];
                    break;

                case "Paid":
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[5];
                    break;
                case "Canceled":
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

            if (chooseWindow.DialogResult.Value)
            {
                bool materialAlreadyAdded = false;
                foreach (List<string> orderline in orderlines)
                {
                    if (orderline[0] == chooseWindow.ChoosenMaterial.Id.ToString())
                    {
                        materialAlreadyAdded = true;
                        break;
                    }
                }

                if (!materialAlreadyAdded)
                {
                    List<string> orderlineNew = new List<string>() { chooseWindow.ChoosenMaterial.Id.ToString(), chooseWindow.ChoosenMaterial.Name, "0" };
                    orderlines.Add(orderlineNew);
                    lvResult.Items.Add(orderlineNew);
                }
            }
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = (ListViewItem)sender;
            var orderline = listViewItem.Content;

            PropertyInfo[] props = orderline.GetType().GetProperties();

            int materialId = int.Parse(props[0].GetValue(orderline, null).ToString());

            List<string> material = control.GetMaterial(materialId);

            /*var listItem = (ListViewItem)sender;
            var orderline = (Orderline) listItem.Content;*/

            ChangeOrderline changeOrderline = new ChangeOrderline(orderline, material[3]);
            changeOrderline.Owner = this;
            changeOrderline.Top = this.Top;
            changeOrderline.Left = this.Left;
            changeOrderline.ShowDialog();

            if (changeOrderline.delete)
            {
                orderlines.Remove(orderlines.Find(x => x[0] == materialId.ToString()));
            }

            GetAllOrderlines();
        }

        private void GetAllOrderlines()
        {
            lvResult.Items.Clear();
            foreach (List<string> item in control.GetOrderlines(orderId))
            {
                lvResult.Items.Add(new { MaterialId = item[0], MaterialName = item[1], Quantity = item[2] });
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            control.UpdateOrder(orderId, orderStatus, orderlines);
        }

        private void CbOrderChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbOrderChoice.SelectedIndex)
            {
                case 0:
                    orderStatus = "Received";
                    break;

                case 1:
                    orderStatus = "InProgress";
                    break;

                case 2:
                    orderStatus = "Done";
                    break;

                case 3:
                    orderStatus = "Shipped";
                    break;

                case 4:
                    orderStatus = "Billed";
                    break;

                case 5:
                    orderStatus = "Paid";
                    break;

                case 6:
                    orderStatus = "Canceled";
                    break;
            }
        }
    }
}
