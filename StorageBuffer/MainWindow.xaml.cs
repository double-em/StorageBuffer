using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StorageBuffer.Application;
using StorageBuffer.Domain;

namespace StorageBuffer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller control;
        private List<OrderWindow> orderWindows;
        public MainWindow()
        {
            InitializeComponent();

            control = new Controller(true);
            orderWindows = new List<OrderWindow>();
            GetAllItems();
            SetupListener();
        }

        private void SetupListener()
        {
        }

        private void GetAllItems()
        {
            foreach (IItem item in control.FindItems("All"))
            {
                lvResult.Items.Add(item);
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

                foreach (IItem item in control.FindItems(criteria, tbSearchBar.Text))
                {
                    lvResult.Items.Add(item);
                }
            }
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var ListItem = (ListViewItem)sender;
            var item = (IItem)ListItem.Content;

            switch (item.Type)
            {
                case "Customer":
                    break;

                case "Material":
                    break;

                case "Order":
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        OrderWindow orderWindow = new OrderWindow((Order)item);
                        orderWindows.Add(orderWindow);
                        orderWindow.Show();
                    }));
                    break;
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            foreach (OrderWindow window in orderWindows)
            {
                window.Close();
            }
        }
    }
}
