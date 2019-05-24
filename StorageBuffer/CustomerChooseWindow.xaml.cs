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

namespace StorageBuffer
{
    public partial class CustomerChooseWindow : Window
    {
        private Controller control;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public CustomerChooseWindow(Controller control)
        {
            InitializeComponent();
            this.control = control;
            GetAllMaterials();
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var listItem = (ListViewItem) sender;
            var ChoosenCustomer = listItem.Content;

            PropertyInfo[] props = ChoosenCustomer.GetType().GetProperties();

            CustomerId = int.Parse(props[0].GetValue(ChoosenCustomer, null).ToString());
            CustomerName = props[1].GetValue(ChoosenCustomer, null).ToString();
            DialogResult = true;
            this.Close();

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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TbSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (control != null)
            {
                lvResult.Items.Clear();
                foreach (List<string> item in control.FindItems("Customers", tbSearchBar.Text))
                {
                    lvResult.Items.Add(new { Id = item[1], Name = item[2], Data = item[3] });
                }
            }
        }

        void GetAllMaterials()
        {
            foreach (List<string> item in control.FindItems("Customers"))
            {
                lvResult.Items.Add(new { Id = item[1], Name = item[2], Data = item[3] });
            }
        }
    }
}
