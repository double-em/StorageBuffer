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
    /// Interaction logic for ChangeOrderline.xaml
    /// </summary>
    public partial class ChangeOrderline : Window
    {
        public bool delete = false;
        private Orderline orderline;
        public ChangeOrderline(Orderline orderline)
        {
            InitializeComponent();
            this.orderline = orderline;
            tbAmount.Text = orderline.Quantity.ToString();
            lChange.Content = "Ændre antal(Max " + orderline.MaterialObj.Quantity + "):";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(tbAmount.Text, out int amount);
            if (amount <= orderline.MaterialObj.Quantity && amount > 0)
            {
                orderline.Quantity = amount;
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            delete = true;
            this.Close();
        }
    }
}
