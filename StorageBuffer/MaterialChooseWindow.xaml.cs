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
    /// Interaction logic for MaterialChooseWindow.xaml
    /// </summary>
    public partial class MaterialChooseWindow : Window
    {
        private Controller control;
        public Material ChoosenMaterial { get; set; }
        public MaterialChooseWindow(Controller control)
        {
            InitializeComponent();
            this.control = control;
            GetAllMaterials();
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var listItem = (ListViewItem) sender;
            ChoosenMaterial = (Material)listItem.Content;
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
                foreach (IItem item in control.FindItems("Materials", tbSearchBar.Text))
                {
                    lvResult.Items.Add(item);
                }
            }
        }

        void GetAllMaterials()
        {
            foreach (IItem item in control.FindItems("Materials"))
            {
                lvResult.Items.Add(item);
            }
        }
    }
}
