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
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class MaterialWindow : Window
    {
        private Controller control;
        private int materialId;

        public MaterialWindow(Controller control, int materialId)
        {
            InitializeComponent();
            this.materialId = materialId;
            this.control = control;
            Setup();
            this.Show();
        }

        private void Setup()
        {
            GetMaterialInfo();
        }

        private void GetMaterialInfo()
        {
            List<string> material = control.GetMaterialLong(materialId);

            tbMaterialName.Text = material[1];
            tbMaterialComment.Text = material[2];
            tbMaterialQuantity.Text = material[3];
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            control.UpdateMaterial(materialId, tbMaterialName.Text, tbMaterialComment.Text, tbMaterialQuantity.Text);
        }
    }
}
