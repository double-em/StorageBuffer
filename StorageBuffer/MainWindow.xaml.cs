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
        public MainWindow()
        {
            InitializeComponent();
            Title = "Storage Buffer";

            control = new Controller();
            GetAllItems();
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
            foreach (IItem item in control.FindItems(cbChoice.SelectedItem.ToString(), tbSearchBar.Text))
            {
                lvResult.Items.Add(item);
            }
        }
    }
}
