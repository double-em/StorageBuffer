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
using System.Xaml;
using StorageBuffer.Domain;

namespace StorageBuffer
{
    /// <summary>
    /// Interaction logic for ChangeOrderline.xaml
    /// </summary>
    public partial class ChangeOrderline : Window
    {
        public bool delete = false;
        private PropertyInfo[] props;
        private int quantity;
        private int maxQuantity;
        public ChangeOrderline(object orderline, string materialQuantity)
        {
            InitializeComponent();

            props = orderline.GetType().GetProperties();
            int.TryParse(props[2].GetValue(orderline, null).ToString(), out quantity);
            int.TryParse(materialQuantity, out maxQuantity);
            tbAmount.Text = quantity.ToString();
            lChange.Content = "Ændre antal(Max " + maxQuantity + "):";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(tbAmount.Text, out int amount);
            if (amount <= maxQuantity && amount > 0)
            {
                quantity = amount;
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
